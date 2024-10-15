using Route.Talabat.Application.Abstraction.Basket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Application.Abstraction.Basket
{
  public  interface  IBasketService
    {

        Task<CustomerBasketDto> GetCustomerBasketAsync(string basketId);
        Task<CustomerBasketDto> UpdateCustomerBasketAsync(CustomerBasketDto customerBasket);
        Task DeleteCustomerBasketAsync(string basketId);


    }
}
