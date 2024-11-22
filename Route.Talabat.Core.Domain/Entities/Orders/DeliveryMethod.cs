using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Domain.Entities.OrderAggregate
{
    public class DeliveryMethod :BaseEntity<int>
    {
        public DeliveryMethod()
        {
            
        }
        public DeliveryMethod(string shortName, string description, decimal cost, string deliveryTime)
        {
            ShortName = shortName;
            Description = description;
            Cost = cost;
            DeliveryTime = deliveryTime;
        }

        public required string ShortName { get; set; }
        public required string Description { get; set; }
        public  decimal Cost { get; set; }
        public required string DeliveryTime { get; set; }



    }
}
