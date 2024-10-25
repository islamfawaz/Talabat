using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Route.Talabat.Application.Abstraction.Auth;
using Route.Talabat.Core.Application.Exception;
using Route.Talabat.Core.Domain.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Route.Talabat.Core.Application.Services.Auth
{
    public class AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings) : IAuthService
    {
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;

        public async Task<UserDto> LoginAsync(LoginDto model)
        {
            // Normalize email to lowercase
            var normalizedEmail = model.Email.ToLower();
            var user = await userManager.FindByEmailAsync(normalizedEmail);

            if (user is null) throw new UnAuthorizedException("Invalid Login attempt");




             
            var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: true);
            if(result.IsNotAllowed) throw new UnAuthorizedException("Account not Confirmed Yet.");


          if(result.IsLockedOut) throw new UnAuthorizedException("Account Is Locked.");

            if (!result.Succeeded) throw new UnAuthorizedException("Invalid Login attempt");

            // Log successful login

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
            // Normalize email and username to lowercase
            var normalizedEmail = model.Email.ToLower();
            var normalizedUserName = model.UserName.ToLower();

            var user = new ApplicationUser()
            {
                DisplayName = model.DisplayName,
                Email = normalizedEmail,
                UserName = normalizedUserName,
                PhoneNumber = model.PhoneNumber
            };
            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new ValidationException() { Errors = result.Errors.Select(E => E.Description) };
            }

            // Log successful registration
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
            // Private Claims
            var privateClaims = new List<Claim>
            {
                new Claim(ClaimTypes.PrimarySid, user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.GivenName, user.DisplayName),
            }.Union(await userManager.GetClaimsAsync(user)).ToList();

            // Add roles to claims
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                privateClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }


            // Create JWT token
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var tokenObj = new JwtSecurityToken(
                audience: _jwtSettings.Audience,
                issuer: _jwtSettings.Issuer,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                claims: privateClaims,
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256));


            return new JwtSecurityTokenHandler().WriteToken(tokenObj);
        }
    }
}
