using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Domain.Entities.Food
{
    public class FoodRating :BaseAuditableEntity<int>
    {
        public int UserId { get; set; }
        public int FoodId { get; set; }
        public int Rating { get; set; } 
        public string? Review { get; set; }
    }
}
