﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Application.Abstraction.Products.Models
{
    public class ProductReturnDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public required string Description { get; set; }

        public string? PictureUrl { get; set; }
        public decimal Price { get; set; }
        public string?Brand { get; set; }
        public int? BrandId { get; set; }

        public string? Category { get; set; }

        public int? CategoryId { get; set; }

    }
}
