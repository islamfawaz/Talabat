using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Route.Talabat.Core.Domain.Contract;
using Route.Talabat.Infrastructure.Persistance.Data;

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
            return services;
        }

        
    }
}
