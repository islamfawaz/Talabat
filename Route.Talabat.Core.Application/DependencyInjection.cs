using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Route.Talabat.Application.Abstraction;
using Route.Talabat.Application.Abstraction.Basket;
using Route.Talabat.Core.Application.Mapping;
using Route.Talabat.Core.Application.Services;
using Route.Talabat.Core.Application.Services.Services;
using Route.Talabat.Core.Domain.Contract.Infrastructure;
namespace Route.Talabat.Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
           services.AddAutoMapper(typeof(MappingProfile));

           services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));
            services.AddScoped(typeof(Func<IBasketService>), (serviceProvider) =>
            {
                var mapper = serviceProvider.GetRequiredService<Mapper>();
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                var basketRepository = serviceProvider.GetRequiredService<IBasketRepository>();
                return new BasketService(basketRepository, mapper, configuration);
            });
            return services;   
        }
    }
}
        