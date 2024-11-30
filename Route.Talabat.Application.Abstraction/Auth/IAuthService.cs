using Route.Talabat.Application.Abstraction.Order.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Application.Abstraction.Auth
{
    public interface IAuthService
    {
        Task<UserDto> LoginAsync(LoginDto model);
        Task<UserDto> RegisterAsync(RegisterDto model);

        Task<UserDto> GetCurrentUser(ClaimsPrincipal claimsPrincipal);

        Task<AddressDto ?>GetUserAddress(ClaimsPrincipal claimsPrincipal);

        Task<AddressDto> UpdateUserAddress(ClaimsPrincipal principal ,AddressDto addressDto);
    }
}
    