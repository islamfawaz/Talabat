using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.Extensions;
using Route.Talabat.APIs.Middlewares;
using Route.Talabat.APIs.Services;
using Route.Talabat.Application.Abstraction;
using Route.Talabat.Application.Abstraction.Abstraction;
using Route.Talabat.Controllers.Errors;
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
            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(option =>
                {
                    //option.SuppressModelStateInvalidFilter = true;
                    option.InvalidModelStateResponseFactory = (actionContext) =>
                    {
                        var groupedErrors =actionContext.ModelState
                       .Where(p => p.Value?.Errors.Count > 0)
                       .GroupBy(p => p.Key, p => p.Value!.Errors.Select(e => e.ErrorMessage))
                       .Select(g => new
                       {
                        Field = g.Key,  
                        Errors = g.SelectMany(e => e)  
                        }).ToList();

                        return new BadRequestObjectResult(new ApiValidationResponse()
                        {
                            Errors = groupedErrors
                        });
                    };
                });
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
            app.UseMiddleware<CustomExceptionHandlerMiddleware>();    
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();  
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
