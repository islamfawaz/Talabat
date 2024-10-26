using Microsoft.AspNetCore.Identity;
using Route.Talabat.Core.Domain.Contract.Persistence.DbInitializer;
using Route.Talabat.Core.Domain.Entities.Identity;
using Route.Talabat.Infrastructure.Persistance.Common;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure.Persistance.Identity
{
    public sealed class StoreIdentityDbInitializer : DbInitializer, IStoreIdentityDbInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
<<<<<<< HEAD:Route.Talabat.Infrastructure.Persistance/Identity/StoreIdentityDbInitializer.cs
        private readonly RoleManager<IdentityRole> _roleManager;

        public StoreIdentityDbInitializer(
            StoreIdentityDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
=======

        public StoreIdentityDbInitializer(StoreIdentityDbContext dbContext, UserManager<ApplicationUser> userManager)
>>>>>>> 91bc098979d5f70837c10c34c9469baf955db4f7:Route.Talabat.Infrastructure.Persistance/_Identity/StoreIdentityDbInitializer.cs
            : base(dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public override async Task SeedAsnc()
        {
<<<<<<< HEAD:Route.Talabat.Infrastructure.Persistance/Identity/StoreIdentityDbInitializer.cs
            // Ensure the "Admin" role exists
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                var adminRole = new IdentityRole("Admin");
                await _roleManager.CreateAsync(adminRole);
            }

            // Check if the user already exists
            var user = await _userManager.FindByEmailAsync("islam5@gmail.com");
            if (user == null)
            {
                // Create the admin user
                user = new ApplicationUser()
=======
            if (!_userManager.Users.Any())
            {

                var user = new ApplicationUser()
>>>>>>> 91bc098979d5f70837c10c34c9469baf955db4f7:Route.Talabat.Infrastructure.Persistance/_Identity/StoreIdentityDbInitializer.cs
                {
                    DisplayName = "Islam Fawaz",
                    UserName = "islam.ahmed",
                    Email = "islam5@gmail.com",
                    PhoneNumber = "0112334455"
                };

<<<<<<< HEAD:Route.Talabat.Infrastructure.Persistance/Identity/StoreIdentityDbInitializer.cs
                var result = await _userManager.CreateAsync(user, "P@ssw0rd");

                if (result.Succeeded)
                {
                    // Assign the "Admin" role to the user
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
            }
            else
            {
                // ensure the user is assigned the Admin role if they exist
                if (!await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
=======
                await _userManager.CreateAsync(user, "P@ssw0rd"); 
>>>>>>> 91bc098979d5f70837c10c34c9469baf955db4f7:Route.Talabat.Infrastructure.Persistance/_Identity/StoreIdentityDbInitializer.cs
            }
        }
    }
}
