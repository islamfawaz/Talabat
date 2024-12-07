using Route.Talabat.Core.Domain.Entities.Basket;

namespace Route.Talabat.Core.Domain.Contract.Infrastructure
{
    public interface IPaymentService
    {
        public  Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string BasketId);

         
    }
}
