using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Application.Abstraction.Basket.Models;
using Route.Talabat.Controllers.Controllers.Base;
using Route.Talabat.Core.Domain.Contract.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Route.Talabat.Controllers.Controllers.Payment
{
    public class PaymentsController :ApiControllerBase
    {
        private readonly IPaymentService paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var result =await paymentService.CreateOrUpdatePaymentIntent(basketId);
            return Ok(result);
        }
    }
}
