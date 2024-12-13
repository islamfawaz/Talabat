using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Domain.Entities.Food
{
    public class Favorite :BaseAuditableEntity<int>
    {
        public string UserId { get; set; }
         public int FoodId { get; set; }
        public virtual FoodItem Food { get; set; }


    }
}
