using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Domain.Entities.OrderAggregate
{
    public class Order  :BaseAuditableEntity<int>
    {
        

        public required string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; }= DateTime.UtcNow;
        public OrderStatus Status { get; set; }=OrderStatus.Pending;
        
      //  public required Address ShippingAddress { get; set; }

        #region Address
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Street { get; set; }

        public required string City { get; set; }
        public required string Country { get; set; }

        #endregion

        public int ? DeliveryMethodId { get; set; }
        public virtual DeliveryMethod ? DeliveryMethod { get; set; }

        public virtual ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();

        public decimal Subtotal { get; set; }

        public decimal Total { get; set; }

        public decimal GetTotal() =>   Subtotal + DeliveryMethod!.Cost;
      
        public string PaymentIntentId { get; set; } = "";
    }
}
