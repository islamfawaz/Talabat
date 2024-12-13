using Infrastructure.Persistence.Services;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Route.Talabat.Core.Domain.Contract.Persistence.DbInitializer;
using Route.Talabat.Core.Domain.Contract.Persistence.Food;
using Route.Talabat.Infrastructure.Persistance.Data;
using Route.Talabat.Infrastructure.Persistance.Data.Interceptors;
using Route.Talabat.Infrastructure.Persistance.Identity;

namespace Route.Talabat.Infrastructure.Persistance
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddPersistanceService(this IServiceCollection services, IConfiguration configuration)
        {
            #region StoreDbContext
            services.AddDbContext<StoreDbContext>((serviceProvider , option) =>
            {
               
                option.UseLazyLoadingProxies()
                      .UseSqlServer(configuration.GetConnectionString("StoreContext")).AddInterceptors(serviceProvider.GetRequiredService<AuditInterceptor>());
            });

            services.AddScoped<IStoreDbInitializer, StoreDbInitializer>();

            services.AddScoped(typeof(AuditInterceptor));

            services.AddScoped<IFoodClassificationService, FoodClassificationService>();

            #endregion


            #region StoreIdentityContext
            services.AddDbContext<StoreIdentityDbContext>((option =>
            {
                option.UseLazyLoadingProxies()
                      .UseSqlServer(configuration.GetConnectionString("IdentityContext"));
            }));

            services.AddScoped(typeof(IStoreIdentityDbInitializer), typeof(StoreIdentityDbInitializer));

            return services;
            #endregion

        }


    }
}
