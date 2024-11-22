using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Domain.Entities.OrderAggregate
{
    public class Order 
    {
        public Order()
        {
            
        }
        public Order(string buyerEmail, Address shippingAddres, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subtotal, decimal total)
        {
            BuyerEmail = buyerEmail;
            ShippingAddres = shippingAddres;
            DeliveryMethod = deliveryMethod;
            Items = items;
            Subtotal = subtotal;
            Total = total; 
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public OrderStatus Status { get; set; }=OrderStatus.Pending;
        public Address ShippingAddres { get; set; }

        //public int DeliveryMethodId { get; set; }
        public virtual DeliveryMethod DeliveryMethod { get; set; }

        public virtual ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();

        public decimal Subtotal { get; set; }

        public decimal Total { get; set; }

        public decimal GetTotal() =>   Subtotal + DeliveryMethod.Cost;
      
        public string PaymentIntentId { get; set; } = "";
    }
}
