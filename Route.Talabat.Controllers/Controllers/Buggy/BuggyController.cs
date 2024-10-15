using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Controllers.Controllers.Base;
using Route.Talabat.Controllers.Errors;
using Route.Talabat.Controllers.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Route.Talabat.Controllers.Controllers.Buggy
{
    public class BuggyController :ApiControllerBase
    {
        [HttpGet("notfound")]
        public IActionResult GetNotFound()
        {
            throw new NotfoundException();
           // return NotFound(new ApiResponse(404));
        }


        [HttpGet("servererror")]
        public IActionResult GetServerError()
        {
                throw new System.Exception();
        }


        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }


        [HttpGet("badrequest/{id}")]
        public IActionResult GetValidationError(int id)
        {
          

            return Ok();
        }

        [HttpGet("unauthorize")]
        public IActionResult GetUnAuthorizeError()
        {
            return Unauthorized(new ApiResponse(401));
        }


        [HttpGet("forbidden")]
        public IActionResult GetForbiddenRequest()
        {
            return Forbid();
        }

        [Authorize]
        [HttpGet("authorized")]
        public IActionResult GetAuthorizeRequest()
        {
            return Ok();
        } 

    }
}
