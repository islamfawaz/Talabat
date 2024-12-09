using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Route.Talabat.Application.Abstraction.Basket;
using Route.Talabat.Application.Abstraction.Order;
using Route.Talabat.Application.Abstraction.Order.Models;
using Route.Talabat.Core.Application.Exception;
using Route.Talabat.Core.Domain.Contract.Infrastructure;
using Route.Talabat.Core.Domain.Contract.Persistence;
using Route.Talabat.Core.Domain.Entities.OrderAggregate;
using Route.Talabat.Core.Domain.Entities.Products;
using Route.Talabat.Core.Domain.Specifications.Orders;

namespace Route.Talabat.Core.Application.Services.Orders
{
    internal class OrderService : IOrderService
    {
        private readonly IBasketService _basketService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;

        public OrderService(IBasketService basketService, IUnitOfWork unitOfWork, IMapper mapper, IPaymentService paymentService)
        {
            _basketService = basketService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _paymentService = paymentService;
        }

        public async Task<OrderToReturnDto> CreateOrderAsync(string buyerEmail, OrderToCreateDto order)
        {
            // 1. Get Basket from Basket Repo
            var basket = await _basketService.GetCustomerBasketAsync(order.BasketId);

            if (basket == null)
                throw new BadRequestException("Basket not found.");

            if (basket.Items == null || !basket.Items.Any())
                throw new BadRequestException("No items in the basket.");

            // 2. Get selected items in the basket from the product repository
            var orderItems = new List<OrderItem>();
            var productRepo = _unitOfWork.GetRepository<Product, int>();

            foreach (var item in basket.Items)
            {
                var product = await productRepo.GetAsync(item.Id);
                if (product == null)
                    throw new BadRequestException($"Product with ID {item.Id} not found.");

                var orderedItem = new OrderItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    PictureUrl = product.PictureUrl ?? "",
                    Price = product.Price,
                    Quantity = item.Quantity,
                };
                orderItems.Add(orderedItem);
            }

            // 3. Calculate SubTotal
            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);

            // 4. Get the delivery method cost
            var deliveryMethodRepo = _unitOfWork.GetRepository<DeliveryMethod, int>();

           
            var deliveryMethod = await deliveryMethodRepo.GetAsync(order.DeliveryMethodId);

            if (deliveryMethod == null)
                throw new BadRequestException("Invalid delivery method selected.");

            // 5. Calculate Total
            var total = subTotal + deliveryMethod.Cost;

            // 6. Handle existing orders with the same PaymentIntentId
            var orderRepo = _unitOfWork.GetRepository<Order, int>();
            if (!string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var spec = new OrderSpecifications(basket.PaymentIntentId);
                var existOrder = await orderRepo.GetAsyncWithSpec(spec);

                if (existOrder != null)
                {
                    orderRepo.Delete(existOrder);
                    await _paymentService.CreateOrUpdatePaymentIntent(basket.Id);
                }
            }
            else
            {
                throw new BadRequestException("PaymentIntentId is missing or invalid.");
            }

            // 7. Create Order
            var orderToCreate = new Order
            {
                BuyerEmail = buyerEmail,
                FirstName = order.ShipToAddress.FirstName,
                LastName = order.ShipToAddress.LastName,
                Street = order.ShipToAddress.Street,
                City = order.ShipToAddress.Street,
                Country = order.ShipToAddress.Country,
                Items = orderItems,
                Subtotal = subTotal,
                DeliveryMethodId = order.DeliveryMethodId,
                Total = total,
                PaymentIntentId = basket.PaymentIntentId
            };

            // 8. Add to database
            await orderRepo.AddAsync(orderToCreate);

            // 9. Save changes to the database
            var created = await _unitOfWork.CompleteAsync() > 0;

            if (!created)
                throw new BadRequestException("An error occurred during order creation.");

            // 10. Return the created order
            return _mapper.Map<OrderToReturnDto>(orderToCreate);
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetOrderForUserAsync(string buyerEmail)
        {
            var spec = new OrderSpecifications(buyerEmail);
            var orders = await _unitOfWork.GetRepository<Order, int>().GetAllAsyncWithSpec(spec);

            return _mapper.Map<IEnumerable<OrderToReturnDto>>(orders);
        }

        public async Task<OrderToReturnDto> GetOrderByIdAsync(string buyerEmail, int id)
        {
            var spec = new OrderSpecifications(buyerEmail, id);

            var order = await _unitOfWork.GetRepository<Order, int>().GetAsyncWithSpec(spec);

            if (order == null)
                throw new NotfoundException(nameof(Order), id);

            return _mapper.Map<OrderToReturnDto>(order);
        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodAsync()
        {
            var deliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();

            return _mapper.Map<IEnumerable<DeliveryMethodDto>>(deliveryMethods);
        }
    }
}
