using AutoMapper;
using Microsoft.Extensions.Configuration;
using Route.Talabat.Application.Abstraction;
using Route.Talabat.Application.Abstraction.Auth;
using Route.Talabat.Application.Abstraction.Basket;
using Route.Talabat.Application.Abstraction.Order;
using Route.Talabat.Application.Abstraction.Products;
using Route.Talabat.Core.Application.Services.Products;
using Route.Talabat.Core.Domain.Contract.Persistence;
using System;
using System.Threading;

namespace Route.Talabat.Core.Application.Services
{
    internal class ServiceManager : IServiceManager
    {
        private readonly Lazy<IOrderService> _orderService;
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBasketService> _basketService;
        private readonly Lazy<IAuthService> _authService;

        public ServiceManager(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IConfiguration configuration,
            Func<IBasketService> basketServiceFactory,
            Func<IAuthService> authServiceFactory,
            Func<IOrderService> orderServiceFactory)
        {
            _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
            _authService = new Lazy<IAuthService>(authServiceFactory);
            _basketService = new Lazy<IBasketService>(basketServiceFactory);
            _orderService = new Lazy<IOrderService>(orderServiceFactory);
        }

        public IProductService ProductService => _productService.Value;

        public IBasketService BasketService => _basketService.Value;

        public IAuthService AuthService => _authService.Value;

        public IOrderService OrderService => _orderService.Value;
    }
}
