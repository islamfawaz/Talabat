using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraStructureService(this IServiceCollection services ,IConfiguration configuration)
        {
            services.AddSingleton(typeof(IConnectionMultiplexer), (serviceProvider) =>
            {
                var connectionString = configuration.GetConnectionString("Radis");
                var connectionMultiplexerObj = ConnectionMultiplexer.Connect(connectionString!);
                return connectionMultiplexerObj;
            } );

            return services;
        }
    }
}
