using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Application.Abstraction;
using Route.Talabat.Application.Abstraction.Basket.Models;
using Route.Talabat.Controllers.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Controllers.Controllers.Basket
{
    public class BasketController :ApiControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public BasketController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async  Task<ActionResult<CustomerBasketDto>>GetBasket(string id)
        {
            var basket=await _serviceManager.BasketService.GetCustomerBasketAsync(id);
            return Ok(basket);

        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasket(CustomerBasketDto basketDto)
        {
            var basket=await _serviceManager.BasketService.UpdateCustomerBasketAsync(basketDto);
            return Ok(basket);

        }







        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
            await _serviceManager.BasketService.DeleteCustomerBasketAsync(id);

        }
    }
}
