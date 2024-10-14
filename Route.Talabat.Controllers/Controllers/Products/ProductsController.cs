﻿using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Application.Abstraction;
using Route.Talabat.Application.Abstraction.Common;
using Route.Talabat.Application.Abstraction.Products.Models;
using Route.Talabat.Controllers.Controllers.Base;
using Route.Talabat.Controllers.Errors;

namespace Route.Talabat.Controllers.Controllers.Products
{ 
    public class ProductsController(IServiceManager serviceManager) : ApiControllerBase
    {
        [HttpGet]//Get/api/Products
        public async Task<ActionResult<Pagination<ProductReturnDto>>> GetProducts([FromQuery]ProductSpecParams specParams)
        {
            var products = await serviceManager.ProductService.GetProductsAsync(specParams);
            return Ok(products);
        }
         
        [HttpGet("{id}")]//Get/api/Products/id
        public async Task<ActionResult<IEnumerable<ProductReturnDto>>> GetProducts(int id)
        {
            var product = await serviceManager.ProductService.GetProductAsync(id);
            if (product == null)
                return NotFound(new ApiResponse(404,$"the Product with Id {id} is not found"));

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
