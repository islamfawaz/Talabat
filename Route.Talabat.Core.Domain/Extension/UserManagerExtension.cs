using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Route.Talabat.Core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Domain.Extension
{
    public static class UserManagerExtension
    {
        public static async Task<ApplicationUser>FindUserWithAddress(this UserManager<ApplicationUser> userManager,ClaimsPrincipal principal)
        {
            var email=principal.FindFirstValue(ClaimTypes.Email);

            var user=await userManager.Users.Where(user=>user.Email==email).Include(user=>user.Address).FirstOrDefaultAsync();

            return user!;
        }
    }
}
