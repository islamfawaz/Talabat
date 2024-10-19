using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Controllers.Controllers.Base;
using Route.Talabat.Controllers.Errors;

namespace Route.Talabat.Controllers.Controllers.Buggy
{
    public class BuggyController : ApiControllerBase
    {
        // Simulate a 404 Not Found error
        [HttpGet("notfound")]
        public IActionResult GetNotFound()
        {
            return NotFound(new ApiResponse(404));  // Return a custom 404 response
        }

        // Simulate a 500 Internal Server Error
        [HttpGet("servererror")]
        public IActionResult GetServerError()
        {
            throw new System.Exception();  // Throw an exception to trigger the middleware
        }

        // Simulate a 400 Bad Request error
        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));  // Return a custom 400 response
        }

        // Simulate a 401 Unauthorized error
        [HttpGet("unauthorize")]
        public IActionResult GetUnAuthorizeError()
        {
            return Unauthorized(new ApiResponse(401));  // Return a custom 401 response
        }

        // Simulate a 403 Forbidden error
        [HttpGet("forbidden")]
        public IActionResult GetForbiddenRequest()
        {
            return Forbid();  // Return a 403 Forbidden response
        }

        // Test for authorized requests only
        [Authorize]
        [HttpGet("authorized")]
        public IActionResult GetAuthorizeRequest()
        {
            return Ok();  // Return a successful response for authorized requests
        }
    }
}
