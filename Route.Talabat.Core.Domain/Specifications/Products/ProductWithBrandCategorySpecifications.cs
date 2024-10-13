﻿using Route.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Domain.Specifications.Products
{
    public class ProductWithBrandCategorySpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandCategorySpecifications(string? sort, int? brandId, int? categoryId ,int pageSize,int pageIndex,string ?search) : base(

              P =>(string.IsNullOrEmpty(search) ||P.NormalizedName.Contains(search))
              &&
                  (brandId == null || P.BrandId == brandId.Value)
                  &&
                  (categoryId == null || P.CategoryId == categoryId.Value))
        {
          

            AddInclude();
            AddOrderBy(P => P.Name);

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "NameDesc":
                        AddOrderByDesc(P => P.Name);
                        break;
                          
                    case "PriceAsc":
                        AddOrderBy(P => P.Price);
                    break;

                    case "PriceDesc":
                        AddOrderByDesc(P => P.Price);
                        break;

                    

                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }

            AddPagination(pageSize * (pageIndex - 1), pageSize);

            
        }

  

        public ProductWithBrandCategorySpecifications(int id) :base(id) 
        {

            AddInclude();

        }

        private protected override void AddInclude()
        {
            base.AddInclude();
            Includes.Add(P => P.Brand!);
            Includes.Add(P => P.Category!);
        }
    }
}