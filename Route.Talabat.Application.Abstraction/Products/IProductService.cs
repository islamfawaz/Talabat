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
        Task<IEnumerable<ProductReturnDto>> GetProductsAsync();
        Task<ProductReturnDto> GetProductAsync(int id);

        Task<IEnumerable<BrandDto>> GetBrandsAsync();


        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
    }
}
