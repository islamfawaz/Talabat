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

            return _mapper.Map<ProductReturnDto>(await _unitOfWork.GetRepository<Product, int>().GetAsyncWithSpec(spec));
            
        }
        public async Task<IEnumerable<ProductReturnDto>> GetProductsAsync()
        {
            var spec = new ProductWithBrandCategorySpecifications();
            spec.Includes.Add(P => P.Brand!);

            return  _mapper.Map<IEnumerable<ProductReturnDto>>(await _unitOfWork.GetRepository<Product, int>().GetAllAsyncWithSpec(spec));
        }
        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
      => _mapper.Map<IEnumerable<BrandDto>>(await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync());
        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        => _mapper.Map<IEnumerable<CategoryDto>>(await _unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync());
    }
}
