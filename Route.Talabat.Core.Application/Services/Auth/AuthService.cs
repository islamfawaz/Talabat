using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Route.Talabat.Application.Abstraction.Auth;
using Route.Talabat.Core.Application.Exception;
using Route.Talabat.Core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Application.Services.Auth
{
    public class AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings) : IAuthService
    {
        
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;

        public async Task<UserDto> LoginAsync(LoginDto model)
        {
            var user=await userManager.FindByEmailAsync(model.Email);

            if (user is null) throw new UnAuthorizedException("Invalid Login attempt");

            var result=await signInManager.CheckPasswordSignInAsync(user,model.Password,lockoutOnFailure:true);
            if (!result.Succeeded) throw new UnAuthorizedException("Invalid Login attempt");

            var response = new UserDto()
            {
                DisplayName = user.DisplayName,
                Id = user.Id,
                Email = model.Email,
                Token = await GenerateTokenAsync(user)

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
                Token = await GenerateTokenAsync(user)
            };

            return response;
        }

        private async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            //Private Claims
            var privateClaims = new List<Claim>
            {
                new Claim(ClaimTypes.PrimarySid,user.Id),
                new Claim(ClaimTypes.Email,user.Email!),
                new Claim(ClaimTypes.GivenName,user.DisplayName),
            }.Union(await userManager.GetClaimsAsync(user)).ToList();


            var roles=await userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                privateClaims.Add(new Claim(ClaimTypes.Role,role.ToString()));   
            }

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var tokenObj = new JwtSecurityToken(

                audience:_jwtSettings.Audience,
                issuer: _jwtSettings.Issuer,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                claims:privateClaims,
                signingCredentials:new SigningCredentials(authKey,SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(tokenObj);

        }

    }
}
