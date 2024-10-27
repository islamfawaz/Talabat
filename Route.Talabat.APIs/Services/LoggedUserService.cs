using Route.Talabat.Application.Abstraction;
using Route.Talabat.Application.Abstraction.Abstraction;
using System.Security.Claims;

namespace Route.Talabat.APIs.Services
{
    public class LoggedUserService : ILoggedUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public string UserId { get; private set; }

        public LoggedUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));

            // Use ClaimTypes.NameIdentifier to retrieve the UserId claim
            UserId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }
    }
}
