using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Controllers.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Controllers.Controllers.Buggy
{
    public class BuggyController :ApiControllerBase
    {
        [HttpGet("notfound")]
        public IActionResult GetNotFound()
        {
            return NotFound(new {StatusCode=404,Message="Not Found"});
        }


        [HttpGet("servererror")]
        public IActionResult GetServerError()
        {
             throw new Exception();
        }


        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new { StatusCode = 400, Message = "Bad Request" });
        }


        [HttpGet("badrequest/{id}")]
        public IActionResult GetValidationError(int id)
        {
            return Ok();
        }

        [HttpGet("unauthorize")]
        public IActionResult GetUnAuthorizeError()
        {
            return Unauthorized(new { StatusCode = 401, Message = "unAuthorize" });
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
