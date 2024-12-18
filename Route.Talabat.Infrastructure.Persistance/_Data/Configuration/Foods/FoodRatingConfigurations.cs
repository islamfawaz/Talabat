using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.Talabat.Core.Domain.Entities.Food;
using Route.Talabat.Infrastructure.Persistance._Data.Configuration.Base;

namespace Route.Talabat.Infrastructure.Persistance._Data.Configuration.Foods
{
    public class FoodRatingConfigurations : BaseAuditableEntityConfigurations<FoodRating, int>
    {
        public override void Configure(EntityTypeBuilder<FoodRating> builder)
        {
            base.Configure(builder);

            builder.ToTable("FoodRatings");

            builder.Property(r => r.UserId)
                .IsRequired();

            builder.Property(r => r.FoodId)
                .IsRequired();

            builder.Property(r => r.Rating)
                .IsRequired()
                .HasPrecision(2, 1); 

            builder.HasIndex(r => new { r.UserId, r.FoodId })
                .IsUnique()
                .HasDatabaseName("IX_FoodRatings_User_Food");
        }
    }
}
