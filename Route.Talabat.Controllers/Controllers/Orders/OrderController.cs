using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Application.Abstraction;
using Route.Talabat.Application.Abstraction.Order.Models;
using Route.Talabat.Controllers.Controllers.Base;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Route.Talabat.Controllers.Controllers.Orders
{
    [Authorize]
    public class OrderController : ApiControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public OrderController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderToCreateDto orderDto)
        {
        

            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

          
            var result = await _serviceManager.OrderService.CreateOrderAsync(buyerEmail!, orderDto);

            return Ok(result);
        }
    }
}
