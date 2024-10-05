using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Domain.Entities.Products
{
    public class Products
    {
        public required string Name { get; set; }

        public required string Description { get; set; }

        public string ? PictureUrl { get; set; }

        public decimal Price { get; set; }

        public ProductBrand ? Brand { get; set; }

        public int ? BrandId { get; set; }

        public ProductCategory ? Category { get; set; }

        public int? CategoryId { get; set; }


    }
}
