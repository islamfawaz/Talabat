using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Route.Talabat.Core.Domain.Contract.Persistence;
using Route.Talabat.Infrastructure.Persistance.Data;
using Route.Talabat.Infrastructure.Persistance.Data.Interceptors;
using Route.Talabat.Infrastructure.Persistance.UnitOfWork;

namespace Route.Talabat.Infrastructure.Persistance
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddPersistanceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreContext>((option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("StoreContext"));
            }));
            
            services.AddScoped<IStoreContextInitializer, StoreContextInitializer>();
            services.AddScoped(typeof(IStoreContextInitializer), typeof(StoreContextInitializer));
            services.AddScoped(typeof(ISaveChangesInterceptor), typeof(BaseAuditableInterceptor));
            return services;
        }

        
    }
}
