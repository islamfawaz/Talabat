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
    public class ClassfiedFoodConfigurations :BaseAuditableEntityConfigurations<ClassifiedFood ,int>
    {
        public override void Configure(EntityTypeBuilder<ClassifiedFood> builder)
        {
            base.Configure(builder);

            builder.Property(f => f.NameFood)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(f => f.Ingredients)
                   .HasMaxLength(2000);

            builder.Property(f => f.Nutrients)
                   .HasMaxLength(500);

            builder.Property(f => f.LinkImage)
                   .HasMaxLength(500);

            builder.Property(f => f.LinkFood)
                   .HasMaxLength(500);

            builder.ToTable("ClassifiedFoods");
        }
    }
}
