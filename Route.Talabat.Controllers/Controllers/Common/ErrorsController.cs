using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Controllers.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Controllers.Controllers.Common
{
    [ApiController]
    [Route("Errors{Code}")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class ErrorsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Error(int Code)
        {
            if (Code == 404)
            {
                var response = new ApiResponse(404, $"The request endPoint :{Request.Path} is not found");
                return NotFound(response);
            }
            return StatusCode(Code ,new ApiResponse(Code));
        }


    }
}
