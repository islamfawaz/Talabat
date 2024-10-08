using Microsoft.Extensions.DependencyInjection;
using Route.Talabat.Application.Abstraction;
using Route.Talabat.Core.Application.Mapping;
using Route.Talabat.Core.Application.Services;
namespace Route.Talabat.Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
           services.AddAutoMapper(typeof(MappingProfile));

           services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));
            
            return services;   
        }
    }
}
