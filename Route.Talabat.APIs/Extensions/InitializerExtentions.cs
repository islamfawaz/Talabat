using Route.Talabat.Core.Domain.Contract.Persistence.DbInitializer;

namespace Route.Talabat.APIs.Extensions
{
    public static class InitializerExtentions
    {
        public static async Task<WebApplication> InitializeDbAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateAsyncScope();
            var services = scope.ServiceProvider;
            var storeContextInitializer = services.GetRequiredService<IStoreDbInitializer>();
            var storeIdentityContextInitializer = services.GetRequiredService<IStoreIdentityDbInitializer>();

            //Ask run tim enviroment for object from store context explicitly

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await storeContextInitializer.InitializerAsync();
                await storeContextInitializer.SeedAsnc();

                await storeIdentityContextInitializer.InitializerAsync();
                await storeIdentityContextInitializer.SeedAsnc();

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "error has been occur during apply migration");

                throw;
            }

            return app;
        }
    }
}
