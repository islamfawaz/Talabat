using AutoMapper;
using Microsoft.Extensions.Options;
using Route.Talabat.Application.Abstraction.Basket.Models;
using Route.Talabat.Core.Application.Exception;
using Route.Talabat.Core.Domain.Contract.Infrastructure;
using Route.Talabat.Core.Domain.Contract.Persistence;
using Route.Talabat.Core.Domain.Entities.Basket;
using Route.Talabat.Core.Domain.Entities.OrderAggregate;
using Route.Talabat.Shared.Models;
using Stripe;
using Product = Route.Talabat.Core.Domain.Entities.Products.Product;

namespace Route.Talabat.Infrastructure.Payment_Service
{
    internal class PaymentService : IPaymentService
    {
        private readonly IBasketRepository basketRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly RedisSettings redisSettings;
        private readonly IMapper mapper;
        private readonly StripeSettings stripeSettings;

        public PaymentService(
            IBasketRepository basketRepository,
            IUnitOfWork unitOfWork,
            IOptions<RedisSettings> redisSettings,
            IMapper mapper,
            IOptions<StripeSettings> stripeSettings)
        {
            this.basketRepository = basketRepository;
            this.unitOfWork = unitOfWork;
            this.redisSettings = redisSettings.Value;
            this.mapper = mapper;
            this.stripeSettings = stripeSettings.Value;
        }

        public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(string BasketId)
        {
            // Set Stripe secret key
            StripeConfiguration.ApiKey = stripeSettings.SecretKey;

            // Fetch basket from repository
            var basket = await basketRepository.GetAsync(BasketId);
            if (basket is null)
                throw new NotfoundException(nameof(CustomerBasket), BasketId);

            // Validate Redis TTL configuration
            if (redisSettings.TimeToLiveInDays <= 0)
                throw new ArgumentException("RedisSettings.TimeToLiveInDays must be greater than 0.");

            var expiration = TimeSpan.FromDays(redisSettings.TimeToLiveInDays);

            // Set Delivery Method (if available)
            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await unitOfWork
                    .GetRepository<DeliveryMethod, int>()
                    .GetAsync(basket.DeliveryMethodId.Value);

                if (deliveryMethod is null)
                    throw new NotfoundException(nameof(DeliveryMethod), basket.DeliveryMethodId.Value);

                basket.ShippingPrice = deliveryMethod!.Cost;
            }

            // Ensure Price Consistency
            if (basket.Items.Count() > 0)
            {
                var productRepo = unitOfWork.GetRepository<Product, int>();
                foreach (var item in basket.Items)
                {
                    var product = await productRepo.GetAsync(item.Id);
                    if (product is null)
                        throw new NotfoundException(nameof(Product), item.Id);

                    if (item.Price != product.Price)
                    {
                        item.Price = product.Price;
                    }
                }
            }

            // Create or update PaymentIntent
            PaymentIntent? paymentIntent = null;
            var paymentIntentService = new PaymentIntentService();

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)(basket.Items.Sum(item => item.Price * item.Quantity) + basket.ShippingPrice),
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                paymentIntent = await paymentIntentService.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)(basket.Items.Sum(item => item.Price * item.Quantity) + basket.ShippingPrice),
                };

                await paymentIntentService.UpdateAsync(basket.PaymentIntentId, options);
            }

            // Update basket in Redis
            await basketRepository.UpdateAsync(basket, expiration);

            // Return updated basket data
            return mapper.Map<CustomerBasketDto>(basket);
        }
    }
}
