using AutoMapper;
using Microsoft.Extensions.Configuration;
using Route.Talabat.Application.Abstraction;
using Route.Talabat.Application.Abstraction.Auth;
using Route.Talabat.Application.Abstraction.Basket;
using Route.Talabat.Application.Abstraction.Products;
using Route.Talabat.Core.Application.Services.Products;
using Route.Talabat.Core.Domain.Contract.Persistence;

namespace Route.Talabat.Core.Application.Services
{
    internal class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBasketService> _basketService;
        private readonly Lazy<IAuthService> _authService;

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper ,IConfiguration configuration,Func<IBasketService> basketServiceFactory ,Func<IAuthService> authServiceFactory)
        {
            
            _productService=new Lazy<IProductService>(()=>new ProductService(unitOfWork,mapper));

            _authService = new Lazy<IAuthService>(authServiceFactory);



            _basketService = new Lazy<IBasketService>(basketServiceFactory);
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;

        }
        public IProductService ProductService => _productService.Value;

        public IBasketService BasketService => _basketService.Value;

        public IAuthService AuthService =>_authService.Value;

    }
}
