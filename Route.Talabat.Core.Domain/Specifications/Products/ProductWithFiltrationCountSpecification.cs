using Route.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Domain.Specifications.Products
{
    public class ProductWithFiltrationCountSpecification:BaseSpecifications<Product,int>
    {
        public ProductWithFiltrationCountSpecification(int? brandId, int? categoryId) : base
            (

            P =>
                 (brandId == null || P.BrandId == brandId.Value)
                 &&
                 (categoryId == null || P.CategoryId == categoryId.Value)
            )
                 
            {
            }
        
    }
}