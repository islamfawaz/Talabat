using Microsoft.AspNetCore.Identity;
using Route.Talabat.Application.Abstraction.Auth;
using Route.Talabat.Core.Domain.Entities.Identity;
using Route.Talabat.Infrastructure.Persistance.Identity;
using Route.Talabat.Core.Application.Services.Auth;

namespace Route.Talabat.APIs.Extensions
{
    public static class IdentityExtentions
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>((identityOptions) =>
            {
                //identityOptions.SignIn.RequireConfirmedAccount = true;
                //identityOptions.SignIn.RequireConfirmedPhoneNumber = true;
                //identityOptions.SignIn.RequireConfirmedEmail = true;

                //identityOptions.Password.RequiredUniqueChars = 2;
                //identityOptions.Password.RequireNonAlphanumeric = true;
                //identityOptions.Password.RequiredLength = 6;
                //identityOptions.Password.RequireDigit = true;
                //identityOptions.Password.RequireLowercase = true;
                //identityOptions.Password.RequireUppercase = true;

                identityOptions.User.RequireUniqueEmail = true;

           //     identityOptions.Lockout.AllowedForNewUsers = true;
             //   identityOptions.Lockout.MaxFailedAccessAttempts = 5;
              //  identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(12);

            })
        .AddEntityFrameworkStores<StoreIdentityDbContext>();
            services.AddScoped(typeof(IAuthService), typeof(AuthService));
            services.Configure<JwtSettings>(configuration.GetSection("JWTSettings"));

            services.AddScoped(typeof(Func<IAuthService>), (servicesProvider) =>
            {
                return ()=>servicesProvider.GetRequiredService<IAuthService>();
            });
            return services;
        }
    }
}
