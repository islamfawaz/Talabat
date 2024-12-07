using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Route.Talabat.Application.Abstraction.Basket.Models
{
    public class CustomerBasketDto
    {
        [Required(ErrorMessage = "The id field is required.")]
        public required string Id { get; set; }

        [Required]
        public IEnumerable<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();

        public string? PaymentIntentId { get; set; }

        public string? ClientSecret { get; set; }

        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }

    }
}
