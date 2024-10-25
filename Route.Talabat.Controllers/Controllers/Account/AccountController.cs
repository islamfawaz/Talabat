using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Application.Abstraction;
using Route.Talabat.Application.Abstraction.Auth;
using Route.Talabat.Controllers.Controllers.Base;

namespace Route.Talabat.Controllers.Controllers.Account
{
    public class AccountController :ApiControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public AccountController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }


        [HttpPost("login")]//POST/api/account/login
        public async Task<ActionResult<UserDto>>Login(LoginDto model)
        {
            var response=await _serviceManager.AuthService.LoginAsync(model);
            return Ok(response);
        }


        [HttpPost("register")]//POST/api/account/register
        public async Task<ActionResult<UserDto>>Register(RegisterDto model)
        {
            var response=await _serviceManager.AuthService.RegisterAsync(model);
            return Ok(response);
        }





    }
}
