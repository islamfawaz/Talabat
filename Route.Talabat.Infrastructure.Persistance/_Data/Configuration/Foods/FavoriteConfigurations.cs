using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.Talabat.Core.Domain.Common;
using Route.Talabat.Core.Domain.Entities.Food;
using Route.Talabat.Infrastructure.Persistance._Data.Configuration.Base;
using Route.Talabat.Infrastructure.Persistance.Data.Configuration.Base;

namespace Route.Talabat.Infrastructure.Persistance._Data.Configuration.Foods
{
    public class FavoriteConfigurations : BaseEntityConfigurations<Favorite, int>
    {
        public override void Configure(EntityTypeBuilder<Favorite> builder)
        {
            base.Configure(builder);

            builder.ToTable("Favorites");

            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id)
                   .IsRequired()
                   .ValueGeneratedOnAdd(); // Automatically generate the ID

            builder.HasOne(f => f.Food)
                   .WithMany()
                   .HasForeignKey(f => f.FoodId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(f => f.UserId)
                   .IsRequired();

            builder.Property(f => f.FoodId)
                   .IsRequired();

            builder.HasIndex(f => new { f.UserId, f.FoodId })
                   .IsUnique()
                   .HasDatabaseName("IX_Favorites_User_Food");
        }
    }
}
