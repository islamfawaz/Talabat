using System;

namespace Route.Talabat.Application.Abstraction.Products.Models
{
    public class ProductSpecParams
    {
        public string? Sort { get; set; }

        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value?.ToUpper(); }  
        }

        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }

        // Ensure PageIndex is at least 1
        private int pageIndex = 1; // Default value
        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value < 1 ? 1 : value; } // Prevent negative or zero PageIndex
        }

        private int pageSize = 5; // Default value
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > 10 ? 10 : (value < 1 ? 1 : value); } // Max 10, Min 1
        }
    }
}
