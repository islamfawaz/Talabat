using AutoMapper;
using Route.Talabat.Application.Abstraction.Products;
using Route.Talabat.Application.Abstraction.Products.Models;
using Route.Talabat.Core.Domain.Contract.Persistence;
using Route.Talabat.Core.Domain.Entities.Products;
using Route.Talabat.Core.Domain.Specifications;
using Route.Talabat.Core.Domain.Specifications.Products;

namespace Route.Talabat.Core.Application.Services.Products
{
    internal class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ProductReturnDto> GetProductAsync(int id)
        {
            var spec = new ProductWithBrandCategorySpecifications(id);
            var product = await _unitOfWork.GetRepository<Product, int>().GetAsyncWithSpec(spec);
            var productToReturn=_mapper.Map<ProductReturnDto>(product);
            return productToReturn;
            
        }
        public async Task<IEnumerable<ProductReturnDto>> GetProductsAsync(string? sort, int? brandId, int? categoryId)
        {
            var spec = new ProductWithBrandCategorySpecifications(sort ,brandId,categoryId);
            var products=await _unitOfWork.GetRepository<Product,int>().GetAllAsyncWithSpec(spec);

            var productsToReturn=_mapper.Map<IEnumerable<ProductReturnDto>>(products);

            return productsToReturn;
               
        }
        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
      => _mapper.Map<IEnumerable<BrandDto>>(await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync());
        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        => _mapper.Map<IEnumerable<CategoryDto>>(await _unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync());
    }
}
