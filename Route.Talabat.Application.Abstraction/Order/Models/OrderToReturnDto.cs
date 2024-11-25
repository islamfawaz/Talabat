using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Application.Abstraction.Order.Models
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public required string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public required string Status { get; set; }


        #region Address
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Street { get; set; }

        public required string City { get; set; }
        public required string Country { get; set; }

        #endregion

        public int? DeliveryMethodId { get; set; }
        public string? DeliveryMethod { get; set; }

        public required ICollection<OrderToReturnDto> Items { get; set; }

        public decimal Subtotal { get; set; }

        public decimal Total { get; set; }
    }
}
