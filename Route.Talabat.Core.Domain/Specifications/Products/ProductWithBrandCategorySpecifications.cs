using Route.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Domain.Specifications.Products
{
    public class ProductWithBrandCategorySpecifications :BaseSpecifications<Product,int>
    {
        public ProductWithBrandCategorySpecifications():base()
        {
            AddInclude();

        }

        private void AddInclude()
        {
            Includes.Add(P => P.Brand!);
            Includes.Add(P => P.Category!);
        }

        public ProductWithBrandCategorySpecifications(int id) :base(id) 
        {

            AddInclude();

        }
    }
}
