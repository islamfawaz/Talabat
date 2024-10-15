using AutoMapper;
using Route.Talabat.Application.Abstraction.Common;
using Route.Talabat.Application.Abstraction.Products;
using Route.Talabat.Application.Abstraction.Products.Models;
using Route.Talabat.Core.Domain.Contract.Persistence;
using Route.Talabat.Core.Domain.Entities.Products;
using Route.Talabat.Core.Domain.Specifications;
using Route.Talabat.Core.Domain.Specifications.Products;
using Route.Talabat.Core.Application.Exception;

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
            if (product is null)
            {
                throw new NotfoundException(nameof(Product), id);
            }
            var productToReturn=_mapper.Map<ProductReturnDto>(product);
            return productToReturn;
            
        }
        public async Task<Pagination<ProductReturnDto>> GetProductsAsync(ProductSpecParams specParams)
        {
            var spec = new ProductWithBrandCategorySpecifications(specParams.Sort, specParams.BrandId, specParams.CategoryId, specParams.PageSize, specParams.PageIndex ,specParams.Search);
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsyncWithSpec(spec);
           
            var specCount=new ProductWithFiltrationCountSpecification(specParams.BrandId, specParams.CategoryId,specParams.Search);
            var data = _mapper.Map<IEnumerable<ProductReturnDto>>(products);
            var count=await _unitOfWork.GetRepository<Product,int>().GetCountAsync(specCount);
            return new Pagination<ProductReturnDto>(specParams.PageIndex, specParams.PageSize,data,count){Data=data};
               
        }
        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
      => _mapper.Map<IEnumerable<BrandDto>>(await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync());
        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        => _mapper.Map<IEnumerable<CategoryDto>>(await _unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync());
    }
}
