using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.Talabat.Core.Domain.Common;
using Route.Talabat.Core.Domain.Entities.Food;
using Route.Talabat.Infrastructure.Persistance._Data.Configuration.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure.Persistance._Data.Configuration.Foods
{
    public class FoodItemConfiguration :BaseAuditableEntityConfigurations<FoodItem,int>
    {

        public override void Configure(EntityTypeBuilder<FoodItem> builder)
        {
            base.Configure(builder);
            builder.ToTable("FoodItems");

            // Configure properties
            builder.Property(f => f.ItemId).IsRequired();
            builder.Property(f => f.UserId).IsRequired();
            builder.Property(f => f.NameFood).HasMaxLength(200).IsRequired();
            builder.Property(f => f.Rating).HasColumnType("decimal(5,2)").IsRequired();
            builder.Property(f => f.LinkImage).HasMaxLength(500);
            builder.Property(f => f.LinkFood).HasMaxLength(500);
            builder.Property(f => f.Ingredients).HasMaxLength(2000);
            builder.Property(f => f.Nutrients).HasMaxLength(1000);
            builder.Property(f => f.ReviewUser).HasMaxLength(1000);

            // Map Label as a computed property (if necessary)
            builder.Property(f => f.Label).HasDefaultValue(false);

            // Configure the primary key (if not inferred)
            builder.HasKey(f => f.ItemId);
        }
    }
}
