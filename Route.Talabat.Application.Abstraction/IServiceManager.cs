using Route.Talabat.Application.Abstraction.Auth;
using Route.Talabat.Application.Abstraction.Basket;
using Route.Talabat.Application.Abstraction.Order;
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
        public IAuthService AuthService { get; }

        public IOrderService OrderService { get; }
    }
}
