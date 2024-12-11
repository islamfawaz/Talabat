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
    public class OrdersController : ApiControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public OrdersController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
         public async Task<ActionResult<OrderToReturnDto>> CreateOrder([FromBody] OrderToCreateDto orderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var result = await _serviceManager.OrderService.CreateOrderAsync(buyerEmail!, orderDto);
            Console.WriteLine($"Received order: {orderDto}");


            return Ok(result);
        }
         
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetOrdersForUser()
        {
            var BuyerEmail=User.FindFirstValue(ClaimTypes.Email);
            var result= await _serviceManager.OrderService.GetOrderForUserAsync(BuyerEmail!);
            return Ok(result);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetOrdersById(int id)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var result = await _serviceManager.OrderService.GetOrderByIdAsync(BuyerEmail!,id);
            return Ok(result);
        }

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethod()
        {
            var result = await _serviceManager.OrderService.GetDeliveryMethodAsync();
            return Ok(result);
        }
    }
}
