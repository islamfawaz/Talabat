using Route.Talabat.Application.Abstraction.Basket.Models;
using Route.Talabat.Core.Domain.Entities.Basket;

namespace Route.Talabat.Core.Domain.Contract.Infrastructure
{
    public interface IPaymentService
    {
        public  Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(string BasketId);

         
    }
}
