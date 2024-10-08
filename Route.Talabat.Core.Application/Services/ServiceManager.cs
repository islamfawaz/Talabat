using AutoMapper;
using Route.Talabat.Application.Abstraction;
using Route.Talabat.Application.Abstraction.Products;
using Route.Talabat.Core.Application.Services.Products;
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
        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productService=new Lazy<IProductService>(()=>new ProductService(unitOfWork,mapper));
        }
        public IProductService ProductService => _productService.Value;
    }
}
