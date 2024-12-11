using Route.Talabat.Core.Domain.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Domain.Specifications.Orders
{
    public class OrderWithPaymentIntentSpecifications :BaseSpecifications<Order,int>
    {
        public OrderWithPaymentIntentSpecifications(string paymentIntentId) 
            : base(order=>order.PaymentIntentId==paymentIntentId) 
        {
            
        }
    }
}
