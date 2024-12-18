using Route.Talabat.Core.Domain.Entities.Food;
using Route.Talabat.Core.Domain.Entities.OrderAggregate;
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

    

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> Brands { get; set; }

        public DbSet<ProductCategory> Categories { get; set; }

        public DbSet<Order>  Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

        public DbSet<FoodRating>  FoodRatings { get; set; }

        public DbSet<ClassifiedFood> ClassifiedFoods { get; set; }
 

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformation).Assembly,
                type => type.GetCustomAttribute<DbContextTypeAttribute>()?.DbContextType == typeof(StoreDbContext)

            );
        }
       
    }
}
