using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Application.Abstraction.Products.Models
{
    public class ProductSpecParams
    {
        public string ? Sort { get; set; }

        public int ? BrandId { get; set; }

        public int? CategoryId { get; set; }

        public int PageIndex { get; set; } = 1;


        private int pageSize;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > 10 ? 10 : value; } 
        }
    }
}
