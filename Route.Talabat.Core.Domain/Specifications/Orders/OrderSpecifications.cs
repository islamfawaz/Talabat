using Route.Talabat.Core.Domain.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Domain.Specifications.Orders
{
    public class OrderSpecifications :BaseSpecifications<Order,int> 
    {
        public OrderSpecifications(string buyerEmail) :base(order=>order.BuyerEmail==buyerEmail)
        {
            AddInclude();
            AddOrderByDesc(order=>order.OrderDate);
        }

        public OrderSpecifications(string buyerEmail ,int orderId) : base(order=>order.Id==orderId && order.BuyerEmail==buyerEmail) 
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
