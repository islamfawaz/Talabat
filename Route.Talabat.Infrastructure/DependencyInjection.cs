using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Route.Talabat.Core.Domain.Contract.Infrastructure;
using Route.Talabat.Infrastructure.Basket_Repositories;
using Route.Talabat.Infrastructure.Payment_Service;
using Route.Talabat.Shared.Models;
using StackExchange.Redis;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraStructureService(this IServiceCollection services, IConfiguration configuration)
    {
        // Redis Configuration
        services.AddSingleton<IConnectionMultiplexer>(serviceProvider =>
        {
            var connectionString = configuration.GetConnectionString("Radis");
            var connectionMultiplexerObj = ConnectionMultiplexer.Connect(connectionString!);
            return connectionMultiplexerObj;
        });

        // Configuring Redis and Stripe settings
        services.Configure<RedisSettings>(configuration.GetSection("RedisSettings"));
        services.Configure<StripeSettings>(configuration.GetSection("StripeSettings"));

        // Add other services
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddScoped<IPaymentService, PaymentService>();

        return services;
    }
}
