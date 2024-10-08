using Route.Talabat.Application.Abstraction.Abstraction;
using System.Security.Claims;

namespace Route.Talabat.APIs.Services
{
    public class LoggedUserService : ILoggedUserService
    {
        private readonly IHttpContextAccessor ? _httpContextAccessor;

        public string UserId {  get; }

        public LoggedUserService(IHttpContextAccessor httpContext)
        {
            _httpContextAccessor = httpContext;
            UserId= _httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }
    }
}
