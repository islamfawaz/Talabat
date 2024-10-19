using Microsoft.AspNetCore.Identity;
using Route.Talabat.Application.Abstraction.Auth;
using Route.Talabat.Core.Domain.Entities.Identity;
using Route.Talabat.Infrastructure.Persistance.Identity;
using Route.Talabat.Core.Application.Services.Auth;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Route.Talabat.APIs.Extensions
{
    public static class IdentityExtentions
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure Identity and define password and lockout rules (optional, commented out)
            services.AddIdentity<ApplicationUser, IdentityRole>((identityOptions) =>
            {
                // identityOptions.Password.RequiredLength = 6;  // Example of enforcing password rules
            })
            .AddEntityFrameworkStores<StoreIdentityDbContext>();  // Add identity store for persistence

            // Set up authentication using JWT
            services.AddAuthentication((authOption) =>
            {
                authOption.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOption.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer((options) =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,  // Validate the recipient (audience) of the token
                    ValidateIssuer = true,    // Validate the token issuer
                    ValidateLifetime = true,  // Ensure the token is not expired
                    ValidateIssuerSigningKey = true,  // Ensure the token is correctly signed
                    ValidAudience = configuration["JWTSettings:Audience"],  // Expected audience from configuration
                    ValidIssuer = configuration["JWTSettings:Issuer"],  // Expected issuer from configuration
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]!)),  // Signing key
                    ClockSkew = TimeSpan.Zero  // Token expiration check will not allow any clock skew
                };
            });

            // Register the authentication service (AuthService) for dependency injection
            services.AddScoped(typeof(IAuthService), typeof(AuthService));
            services.Configure<JwtSettings>(configuration.GetSection("JWTSettings"));  // Configure JWT settings

            services.AddScoped(typeof(Func<IAuthService>), (servicesProvider) =>
            {
                return () => servicesProvider.GetRequiredService<IAuthService>();
            });

            return services;
        }
    }
}
