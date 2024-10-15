using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Application.Abstraction.Basket.Models
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]

        public required string ProductName { get; set; }

        public string? PictureUrl { get; set; }

        [Required]
        [Range(.0,double.MaxValue ,ErrorMessage ="Price must be more than zero")]
            
        public decimal Price { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "quantity must be more than one item")]

        public int Quantity { get; set; }

        public string? Brand { get; set; }

        public string? Category { get; set; }


    }
}
