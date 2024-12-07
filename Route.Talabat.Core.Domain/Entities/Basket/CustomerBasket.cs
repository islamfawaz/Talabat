using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Domain.Entities.Basket
{
    public class CustomerBasket :BaseEntity<string>
    {
        public IEnumerable<BasketItem> Items { get; set; } = new List<BasketItem>();

        public string ?PaymentIntentId  { get; set; }

        public string ? ClientSecret { get; set; }

        public int ? DeliveryMethodId { get; set; }

        public decimal  ShippingPrice { get; set; }



    }
}
