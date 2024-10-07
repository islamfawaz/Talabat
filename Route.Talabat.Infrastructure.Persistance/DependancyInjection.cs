using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            return services;
        }

        
    }
}
