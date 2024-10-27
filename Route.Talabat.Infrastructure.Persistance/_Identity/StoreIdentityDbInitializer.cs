using Microsoft.AspNetCore.Identity;
using Route.Talabat.Core.Domain.Contract.Persistence.DbInitializer;
using Route.Talabat.Core.Domain.Entities.Identity;
using Route.Talabat.Infrastructure.Persistance.Common;
using Route.Talabat.Infrastructure.Persistance.Identity;

public sealed class StoreIdentityDbInitializer : DbInitializer, IStoreIdentityDbInitializer
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public StoreIdentityDbInitializer(StoreIdentityDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        : base(dbContext)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public override async Task SeedAsnc()
    {
        if (!await _roleManager.RoleExistsAsync("Admin"))
        {
            var adminRole = new IdentityRole("Admin");
            await _roleManager.CreateAsync(adminRole);
        }

        var user = await _userManager.FindByEmailAsync("islam5@gmail.com");
        if (user == null)
        {
            user = new ApplicationUser
            {
                DisplayName = "Islam Fawaz",
                UserName = "islam.ahmed",
                Email = "islam5@gmail.com",
                PhoneNumber = "0112334455"
            };

            var result = await _userManager.CreateAsync(user, "P@ssw0rd");

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }
        }
        else
        {
            if (!await _userManager.IsInRoleAsync(user, "Admin"))
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
