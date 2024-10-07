
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Route.Talabat.APIs.Extensions;
using Route.Talabat.Core.Domain.Contract;
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

            #region  DataBase Initialize and Data Seeds
           await  app.InitializeStoreContextAsync();
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
