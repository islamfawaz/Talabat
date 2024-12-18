using Route.Talabat.Core.Domain.Entities.OrderAggregate;

namespace Route.Talabat.Core.Domain.Specifications.Orders
{
    public class OrderSpecifications : BaseSpecifications<Order, int>
    {
        public OrderSpecifications(string buyerEmail ,int orderId)
            : base(order=>order.Id==orderId && order.BuyerEmail==buyerEmail)
            
        {
            AddInclude(); 
        }

        public OrderSpecifications(string buyerEmail) 
            :base(order=>order.BuyerEmail == buyerEmail)
        {
            AddInclude();
        }

        private protected override void AddInclude()
        {
            base.AddInclude();
            Includes.Add(order => order.Items);  
            Includes.Add(order => order.DeliveryMethod!); 
        }
    }
}
