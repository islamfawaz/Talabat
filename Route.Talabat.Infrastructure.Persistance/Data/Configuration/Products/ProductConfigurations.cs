using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.Talabat.Core.Domain.Entities.Products;
using Route.Talabat.Infrastructure.Persistance.Data.Configuration.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure.Persistance.Data.Configuration.Products
{
    internal class ProductConfigurations : BaseEntityConfigurations<Product,int>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder.Property(P =>P.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(P => P.NormalizedName)
               .IsRequired()
               .HasMaxLength(100);

            builder.Property(P => P.Description)
                .IsRequired();

            builder.Property(P => P.Price)
                .HasColumnType("decimal(9,2)");

            builder.HasOne(P => P.Brand)
                .WithMany()
                .HasForeignKey(P => P.BrandId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(P=>P.Category)
                .WithMany()
                .HasForeignKey(P=>P.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
