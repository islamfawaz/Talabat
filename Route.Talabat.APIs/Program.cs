using Route.Talabat.APIs.Extensions;
using Route.Talabat.APIs.Services;
using Route.Talabat.Application.Abstraction;
using Route.Talabat.Application.Abstraction.Abstraction;
using Route.Talabat.Core.Application;
using Route.Talabat.Core.Domain.Contract.Persistence;
using Route.Talabat.Infrastructure.Persistance;
using Route.Talabat.Infrastructure.Persistance.UnitOfWork;

namespace Route.Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            #region Configure Services
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddPersistanceService(builder.Configuration);
            builder.Services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<ILoggedUserService, LoggedUserService>();
            builder.Services.AddControllers().AddApplicationPart(typeof(Controllers.AssemblyInformation).Assembly);
            builder.Services.AddApplicationService();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #endregion

            var app = builder.Build();

            #region  Database Initialize and Data Seeds
            await app.InitializeStoreContextAsync();
            #endregion

            #region Configure Kestrel Middlewares
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
