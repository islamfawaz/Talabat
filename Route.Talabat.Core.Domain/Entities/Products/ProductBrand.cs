
using System.ComponentModel.DataAnnotations;

namespace Route.Talabat.Core.Domain.Entities.Products
{
    public class ProductBrand :BaseAuditableEntity<int>
    {
        [Required(ErrorMessage = "Brand name is required.")]
        [StringLength(100, ErrorMessage = "Brand name can't be longer than 100 characters.")]
        public required string Name { get; set; }
      
    }
}
