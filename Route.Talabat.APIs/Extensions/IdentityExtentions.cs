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
            services.AddAuthentication((authOption) =>
            {
                authOption.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOption.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer((options) =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime=true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = configuration["JWTSettings:Audience"],
                        ValidIssuer = configuration["JWTSettings:Talabat"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]!)),
                        ClockSkew=TimeSpan.Zero
                        

                    };
                }).AddJwtBearer("Bearer02")
                ;
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
