using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Application.Abstraction;
using Route.Talabat.Application.Abstraction.Auth;
using Route.Talabat.Application.Abstraction.Order.Models;
using Route.Talabat.Controllers.Controllers.Base;
using System.Security.Claims;

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

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>>GetCurrentUser()
        {
            var result=await _serviceManager.AuthService.GetCurrentUser(User);
            return Ok(result);
        }
        [HttpGet("getUserAddress")]
        [Authorize]
        public async Task<ActionResult<AddressDto>>GetUserAddress()
        {
            var result= await _serviceManager.AuthService.GetUserAddress(User);
            return Ok(result);
        }

        [HttpPut("updateUserAddress")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress([FromBody] AddressDto addressDto)
        {
            var result = await _serviceManager.AuthService.UpdateUserAddress(User, addressDto);
            return Ok(result);
        }
        [HttpGet("emailExist")]
        [Authorize]
        public async Task<ActionResult<bool>> CheckEmailExist()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await _serviceManager.AuthService.EmailExist(email!));

        }


    }
}
