using Route.Talabat.Core.Domain.Entities.Products;
using Route.Talabat.Infrastructure.Persistance.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure.Persistance.Data
{
    public class StoreDbContext :DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> dbContext): base(dbContext)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(
                typeof(AssemblyInformation).Assembly,
                type => type.GetCustomAttributes(typeof(DbContextTypeAttribute), false)
                            .OfType<DbContextTypeAttribute>()
                            .Any(attr => attr.DbContextType == typeof(StoreDbContext))
            );
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> Brands { get; set; }

        public DbSet<ProductCategory> Categories { get; set; }

    }
}
