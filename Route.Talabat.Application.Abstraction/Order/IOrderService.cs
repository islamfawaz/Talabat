using Route.Talabat.Application.Abstraction.Order.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Application.Abstraction.Order
{
     interface IOrderService
    {
        Task<OrderToReturnDto> CreateOrderAsync(string buyerEmail, OrderToCreateDto order);
        Task<OrderToReturnDto> GetOrderByIdAsync(string buyerEmail,int id);

        Task<OrderToReturnDto> GetOrderForUserAsync(string buyerEmail);

        Task<DeliveryMethodDto> GetDeliveryMethodAsync();


    }
}
