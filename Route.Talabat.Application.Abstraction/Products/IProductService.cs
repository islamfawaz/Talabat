﻿using Route.Talabat.Application.Abstraction.Common;
using Route.Talabat.Application.Abstraction.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Application.Abstraction.Products
{
    public interface IProductService
    {
        Task<Pagination<ProductReturnDto>> GetProductsAsync(ProductSpecParams productSpec);
        Task<ProductReturnDto> GetProductAsync(int id);

        Task<IEnumerable<BrandDto>> GetBrandsAsync();

       

        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
    }
}
