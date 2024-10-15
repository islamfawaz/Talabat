using Route.Talabat.Application.Abstraction.Basket;
using Route.Talabat.Application.Abstraction.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Application.Abstraction
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }

        public IBasketService  BasketService { get; }
    }
}
