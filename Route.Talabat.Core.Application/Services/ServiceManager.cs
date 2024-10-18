using AutoMapper;
using Microsoft.Extensions.Configuration;
using Route.Talabat.Application.Abstraction;
using Route.Talabat.Application.Abstraction.Auth;
using Route.Talabat.Application.Abstraction.Basket;
using Route.Talabat.Application.Abstraction.Employee;
using Route.Talabat.Application.Abstraction.Products;
using Route.Talabat.Core.Application.Services.Auth;
using Route.Talabat.Core.Application.Services.Employees;
using Route.Talabat.Core.Application.Services.Products;
using Route.Talabat.Core.Application.Services.Services;
using Route.Talabat.Core.Domain.Contract.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Application.Services
{
    internal class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IEmployeeService> _employeeService;
        private readonly Lazy<IBasketService> _basketService;
        private readonly Lazy<IAuthService> _authService;

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper ,IConfiguration configuration,Func<IBasketService> basketServiceFactory ,Func<IAuthService> authServiceFactory)
        {
            
            _productService=new Lazy<IProductService>(()=>new ProductService(unitOfWork,mapper));
            _employeeService = new Lazy<IEmployeeService> (() => new EmployeeService(unitOfWork, mapper));
            _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(unitOfWork, mapper));

            _authService = new Lazy<IAuthService>(authServiceFactory);



            _basketService = new Lazy<IBasketService>(basketServiceFactory);
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;

        }
        public IProductService ProductService => _productService.Value;
        public IEmployeeService EmployeeService => _employeeService.Value;

        public IBasketService BasketService => _basketService.Value;

        public IAuthService AuthService =>_authService.Value;

    }
}
