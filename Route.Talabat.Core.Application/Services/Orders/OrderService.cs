using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Route.Talabat.Application.Abstraction.Basket;
using Route.Talabat.Application.Abstraction.Order;
using Route.Talabat.Application.Abstraction.Order.Models;
using Route.Talabat.Core.Application.Exception;
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

        public OrderService(IBasketService basketService ,IUnitOfWork unitOfWork ,IMapper mapper)
        {
            _basketService = basketService;
            _unitOfWork = unitOfWork;
           _mapper = mapper;
        }
        public async Task<OrderToReturnDto> CreateOrderAsync(string buyerEmail, OrderToCreateDto order)
        {
            // 1.Get Basket from Basket Repo
            var basket =await _basketService.GetCustomerBasketAsync(order.BasketId);

            //2. Get selected item at basket from product Repo

            var orderItem = new List<OrderItem>();

            if(basket.Items.Count()>0)
            {
                var productRepo =  _unitOfWork.GetRepository<Product,int>();
                foreach (var item in basket.Items)
                {
                    var product =await productRepo.GetAsync(item.Id);
                    if (product is not null)
                    {
                        var orderedItem = new OrderItem() { 
                            ProductId=product.Id,
                            ProductName=product.Name,
                            PictureUrl=product.PictureUrl ??"",  
                            
                            Price=product.Price,
                            Quantity=item.Quantity,
                        };
                        orderItem.Add(orderedItem);

                    }
                }
            }

            //3.Calculate SubTotal
            var subTotal=orderItem.Sum(item=>item.Price*item.Quantity);

            //4.Create Order
            var orderTocreate = new Order()
            {
                BuyerEmail = buyerEmail,

                FirstName=order.FirstName,
                LastName=order.LastName,
                Street=order.Street,
                City=order.City,
                Country=order.Country,

                Items=orderItem,
                Subtotal=subTotal,
                DeliveryMethodId=order.DeliveryMethodId,

            };

           await _unitOfWork.GetRepository<Order, int>().AddAsync(orderTocreate);
            //5 Save to DataBase
            var created = await _unitOfWork.CompleteAsync() >0;
            if (!created)
                throw new BadRequestException("an error has occured during creating order");

            return _mapper.Map<OrderToReturnDto>(orderTocreate);
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
