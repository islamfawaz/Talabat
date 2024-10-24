using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Route.Talabat.Core.Domain.Entities.Identity;
using Route.Talabat.Infrastructure.Persistance.Common;
using Route.Talabat.Infrastructure.Persistance.Data;
using Route.Talabat.Infrastructure.Persistance.Identity.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure.Persistance.Identity
{
    public class StoreIdentityDbContext :IdentityDbContext<ApplicationUser>
    {
        public StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(
                typeof(AssemblyInformation).Assembly,
                type => type.GetCustomAttributes(typeof(DbContextTypeAttribute), false)
                            .OfType<DbContextTypeAttribute>()
                            .Any(attr => attr.DbContextType == typeof(StoreIdentityDbContext))
            );
        }
    }
}
