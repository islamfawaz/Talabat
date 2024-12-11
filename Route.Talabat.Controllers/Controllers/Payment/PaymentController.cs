using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Application.Abstraction.Basket.Models;
using Route.Talabat.Controllers.Controllers.Base;
using Route.Talabat.Core.Domain.Contract.Infrastructure;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ApiControllerBase
{
    private readonly IPaymentService paymentService;

    public PaymentsController(IPaymentService paymentService)
    {
        this.paymentService = paymentService;
    }

    [Authorize]
    [HttpPost("{basketId}")]
    public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string basketId)
    {
        var result = await paymentService.CreateOrUpdatePaymentIntent(basketId);
        return Ok(result);
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> HandleWebhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        var sigHeader = Request.Headers["Stripe-Signature"];
        await paymentService.UpdateOrderPaymentStatus(json, sigHeader!);


       
        return Ok();
    }
}
