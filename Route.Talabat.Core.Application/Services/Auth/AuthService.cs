using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Route.Talabat.Application.Abstraction.Auth;
using Route.Talabat.Core.Application.Exception;
using Route.Talabat.Core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Application.Services.Auth
{
    internal class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AuthService(UserManager<ApplicationUser> userManager ,SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public async Task<UserDto> LoginAsync(LoginDto model)
        {
            var user=await userManager.FindByEmailAsync(model.Email);

            if (user is null) throw new BadRequestException("Invalid Login");

            var result=await signInManager.CheckPasswordSignInAsync(user,model.Password,lockoutOnFailure:true);
            if (!result.Succeeded) throw new BadRequestException("Invalid Login");

            var response = new UserDto()
            {
                DisplayName = user.DisplayName,
                Id = user.Id,
                Email = model.Email,
                Token = "This will jwt token"

            };
           
            return response;

        }

        public async Task<UserDto> RegisterAsync(RegisterDto model)
        {
            var user = new ApplicationUser()
            {
                DisplayName= model.DisplayName,
                 Email = model.Email,
                 UserName = model.UserName,
                 PhoneNumber = model.PhoneNumber,
            };

            var result=await userManager.CreateAsync(user);

            if(!result.Succeeded) throw new ValidationException() { Errors=result.Errors.Select(E=>E.Description)};


            var response = new UserDto()
            {
                DisplayName = user.DisplayName,
                Id = user.Id,
                Email = model.Email,
                Token = "This will jwt token"

            };

            return response;
        }
    }
}
