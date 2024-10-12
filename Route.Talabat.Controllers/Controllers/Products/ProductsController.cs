using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Application.Abstraction;
using Route.Talabat.Application.Abstraction.Products.Models;
using Route.Talabat.Controllers.Controllers.Base;

namespace Route.Talabat.Controllers.Controllers.Products
{ 
    public class ProductsController(IServiceManager serviceManager) : ApiControllerBase
    {
        [HttpGet]//Get/api/Products
        public async Task<ActionResult<IEnumerable<ProductReturnDto>>> GetProducts(string ? sort ,int ?brandId,int ?categoryId)
        {
            var products = await serviceManager.ProductService.GetProductsAsync(sort,brandId,categoryId);
            return Ok(products);
        }
         
        [HttpGet("{id}")]//Get/api/Products/id
        public async Task<ActionResult<IEnumerable<ProductReturnDto>>> GetProducts(int id)
        {
            var product = await serviceManager.ProductService.GetProductAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }
           
        [HttpGet("brands")]//Get:/api/Products/brands
        public async Task<ActionResult<IEnumerable<BrandDto>>>GetBrands()
        {
            var brands =await serviceManager.ProductService.GetBrandsAsync();
            return Ok(brands);  
        }



        [HttpGet("categories")]//Get:/api/Products/brands
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await serviceManager.ProductService.GetCategoriesAsync();
            return Ok(categories);
        }   

    }
}
