
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Route.Talabat.Infrastructure.Persistance;
using Route.Talabat.Infrastructure.Persistance.Data;

namespace Route.Talabat.APIs
{
    public class Program
    {
        public static  async Task Main(string[] args)
        {
            #region Configure Services
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddPersistanceService(builder.Configuration);         

            #endregion

            var app = builder.Build();

            #region Update DataBase and Data Seeds
            using var scope = app.Services.CreateAsyncScope();
            var services = scope.ServiceProvider;
            var dbcontext = services.GetRequiredService<StoreContext>();
            //Ask run tim enviroment for object from store context explicitly

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                var pendingMigration = dbcontext.Database.GetPendingMigrations();
                if (pendingMigration.Any())
                    await dbcontext.Database.MigrateAsync();

              await StoreContextSeed.SeedAsync(dbcontext);

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "error has been occur during apply migration");

                throw;
            } 
            #endregion

            #region Configure Kestrel Middlewares
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
             

            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
