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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Application.Services.Orders
{
    internal class OrderService : IOrderService
    {
        private readonly IBasketService _basketService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPaymentService paymentService;

        public OrderService(IBasketService basketService ,IUnitOfWork unitOfWork ,IMapper mapper ,IPaymentService paymentService)
        {
            _basketService = basketService;
            _unitOfWork = unitOfWork;
           _mapper = mapper;
            this.paymentService = paymentService;
        }

        public async Task<OrderToReturnDto> CreateOrderAsync(string buyerEmail, OrderToCreateDto order )
        {
            // 1. Get Basket from Basket Repo
            var basket = await _basketService.GetCustomerBasketAsync(order.BasketId);

            // 2. Get selected items in the basket from the product repository
            var orderItems = new List<OrderItem>();

            if (basket.Items.Any())
            {
                var productRepo = _unitOfWork.GetRepository<Product, int>();
                foreach (var item in basket.Items)
                {
                    var product = await productRepo.GetAsync(item.Id);
                    if (product != null)
                    {
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
                }
            }

            // 3. Calculate SubTotal
            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);

            // 4. Get the delivery method cost
            var deliveryMethodRepo = _unitOfWork.GetRepository<DeliveryMethod, int>();
            var deliveryMethod = await deliveryMethodRepo.GetAsync(order.DeliveryMethodId);

            if (deliveryMethod == null)
            {
                throw new BadRequestException("Invalid delivery method selected.");
            }

            // 5. Calculate Total
            var total = subTotal + deliveryMethod.Cost;

            var orderRepo = _unitOfWork.GetRepository<Order, int>();

            var spec = new OrderSpecifications(basket.PaymentIntentId!);

            var existOrder = await orderRepo.GetAsyncWithSpec(spec);

            if (existOrder is not null)
            {
                orderRepo.Delete(existOrder);
               await paymentService.CreateOrUpdatePaymentIntent(basket.Id );
            }
            // 6. Create Order
            var orderToCreate = new Order
            {
                BuyerEmail = buyerEmail,
                FirstName = order.FirstName,
                LastName = order.LastName,
                Street = order.Street,
                City = order.City,
                Country = order.Country,
                Items = orderItems,
                Subtotal = subTotal,
                DeliveryMethodId = order.DeliveryMethodId,
                Total = total,
                PaymentIntentId=basket.PaymentIntentId!
            };

           

            // 7. Add to database
            await orderRepo.AddAsync(orderToCreate);

            // 8. Save changes to the database
            var created = await _unitOfWork.CompleteAsync() > 0;
            if (!created)
            {
                throw new BadRequestException("An error occurred during order creation.");
            }

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

            if (order is null)
                throw new NotfoundException(nameof(Order),id);


            return _mapper.Map<OrderToReturnDto>(order);
        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodAsync()
        {
            var deliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();

            return _mapper.Map<IEnumerable<DeliveryMethodDto>>(deliveryMethods);
        }

      
    }
}
