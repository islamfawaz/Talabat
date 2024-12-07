using Microsoft.Extensions.Options;
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
        private readonly IOptions<RedisSettings> options;

        public PaymentService(IBasketRepository basketRepository ,IUnitOfWork unitOfWork ,IOptions<RedisSettings> options) 
        {
            this.basketRepository = basketRepository;
            this.unitOfWork = unitOfWork;
            this.options = options;
        }
        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string BasketId)
        {

            var basket=await basketRepository.GetAsync(BasketId);
            if (basket is null)throw new NotfoundException(nameof(CustomerBasket),BasketId);



             // Set Delivery method
            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAsync(basket.DeliveryMethodId.Value);
                if (deliveryMethod is null) throw new NotfoundException(nameof (DeliveryMethod),basket.DeliveryMethodId.Value);
                basket.ShippingPrice = deliveryMethod!.Cost;


            }    

            //Ensure the Price
            if (basket.Items.Count()>0)
            {
                var productRepo = unitOfWork.GetRepository<Product, int>();
                foreach (var item in basket.Items)
                {
                    var product =await productRepo.GetAsync(item.Id);
                    if (product is null) throw new NotfoundException(nameof(Product), item.Id);

                    if (item.Price != product.Price)
                    {
                        item.Price = product.Price;
                    }
                }
            }

            PaymentIntent ? paymentIntent = null;
            PaymentIntentService paymentIntentService =new PaymentIntentService();

            if (string.IsNullOrEmpty(basket.PaymentIntentId)) //Create new PaymentIntent
            {
                var option = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.Items.Sum(item => item.Price * item.Quantity) +(long) basket.ShippingPrice,
                    Currency="USD",
                    PaymentMethodTypes=new List<string>() { "Card"}
                };

             paymentIntent=   await paymentIntentService.CreateAsync(option); //Integration With Stripe
             basket.PaymentIntentId= paymentIntent.Id;
             basket.ClientSecret = paymentIntent.ClientSecret;

            }

            else //Update an Existing Intent Payment Intent
            {
                var option = new PaymentIntentUpdateOptions
                {
                    Amount = (long)basket.Items.Sum(item => item.Price * item.Quantity) + (long)basket.ShippingPrice,
                };
                await paymentIntentService.UpdateAsync(basket.PaymentIntentId, option);//Integration With Stripe
            }

            await basketRepository.UpdateAsync(basket, TimeSpan.FromDays(options.Value.TimeToLiveInDays));

            return basket;

        }
    }
}
