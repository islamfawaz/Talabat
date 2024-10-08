using AutoMapper;
using Route.Talabat.Application.Abstraction.Products;
using Route.Talabat.Application.Abstraction.Products.Models;
using Route.Talabat.Core.Domain.Contract.Persistence;
using Route.Talabat.Core.Domain.Entities.Products;

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
            => _mapper.Map<ProductReturnDto>(await _unitOfWork.GetRepository<Product, int>().GetAsync(id));
        public async Task<IEnumerable<ProductReturnDto>> GetProductsAsync()
            => _mapper.Map<IEnumerable<ProductReturnDto>>(await _unitOfWork.GetRepository<Product, int>().GetAllAsync());
        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
      => _mapper.Map<IEnumerable<BrandDto>>(await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync());
        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        => _mapper.Map<IEnumerable<CategoryDto>>(await _unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync());
    }
}
