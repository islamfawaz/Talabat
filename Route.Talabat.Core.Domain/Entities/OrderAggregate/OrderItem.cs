using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Domain.Entities.OrderAggregate
{
    public class OrderItem:BaseAuditableEntity<int>
    {
        public OrderItem()
        {
            
        }

        public OrderItem(ProductItemOrder product, decimal price, int qountity)
        {
            Product = product;
            Price = price;
            Qountity = qountity;
        }

        public ProductItemOrder Product { get; set; }
        public decimal Price { get; set; }

        public int Qountity { get; set; }

    }
}
