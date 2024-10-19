using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Route.Talabat.Core.Domain.Entities.Identity;
using Route.Talabat.Infrastructure.Persistance.Data;
using Route.Talabat.Infrastructure.Persistance.Identity;

namespace Route.Talabat.Dashboard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            #region StoreDbContext
            builder.Services.AddDbContext<StoreDbContext>((option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("StoreContext"));
            }));
            #endregion

            #region StoreIdentityContext
            builder.Services.AddDbContext<StoreIdentityDbContext>((option =>
            {
                option
                      .UseSqlServer(builder.Configuration.GetConnectionString("IdentityContext"));
            }));
            #endregion

            #region Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>((identityOptions) =>
            {
                identityOptions.User.RequireUniqueEmail=true;
            })


           .AddEntityFrameworkStores<StoreIdentityDbContext>();
            #endregion
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
