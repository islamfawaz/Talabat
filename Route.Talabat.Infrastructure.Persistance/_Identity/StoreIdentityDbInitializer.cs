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

        public StoreIdentityDbInitializer(StoreIdentityDbContext dbContext, UserManager<ApplicationUser> userManager)
            : base(dbContext)
        {
            _userManager = userManager;
        }

        public override async Task SeedAsnc()
        {
            if (!_userManager.Users.Any())
            {

                var user = new ApplicationUser()
                {
                    DisplayName = "Islam Fawaz",
                    UserName = "islam.ahmed",
                    Email = "islam5@gmail.com",
                    PhoneNumber = "0112334455"
                };

                await _userManager.CreateAsync(user, "P@ssw0rd"); 
            }
        }
    }
}
