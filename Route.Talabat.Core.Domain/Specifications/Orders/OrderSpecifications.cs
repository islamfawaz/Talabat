using Route.Talabat.Core.Domain.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Domain.Specifications.Orders
{
    public class OrderSpecifications : BaseSpecifications<Order, int>
    {
        // Constructor with optional parameters
        public OrderSpecifications(string buyerEmail = null, int? orderId = null, string paymentIntentId = null)
            : base(order =>
                (buyerEmail == null || order.BuyerEmail == buyerEmail) &&
                (orderId == null || order.Id == orderId) &&
                (paymentIntentId == null || order.PaymentIntentId == paymentIntentId)
            )
        {
            AddInclude(); // Include related entities like items and delivery method

            // If buyerEmail is provided, order by descending order date
            if (!string.IsNullOrEmpty(buyerEmail))
                AddOrderByDesc(order => order.OrderDate);
        }

        // Helper method to add includes for related entities
        private protected void AddInclude()
        {
            Includes.Add(order => order.Items); // Include order items
            Includes.Add(order => order.DeliveryMethod!); // Include delivery method, assuming non-nullable
        }
    }
}
