using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.Talabat.Core.Domain.Entities.OrderAggregate;
using Route.Talabat.Infrastructure.Persistance.Data.Configuration.Base;

namespace Route.Talabat.Infrastructure.Persistance._Data.Configuration.Orders
{
    internal class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(O => O.ShippingAddres, ShippingAddres => ShippingAddres.WithOwner()); // 1 : 1 [Total]
            builder.Property(order => order.Status).
                HasConversion(
                 (OStatus) => OStatus.ToString()
                , (OStatus) => (OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus)

                );
            builder.Property(order => order.Subtotal).HasColumnType("decimal(2,8");
            builder.HasOne(order => order.DeliveryMethod)
                .WithMany()
                .HasForeignKey(order=>order.DeliveryMethodId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(order=>order.Items ).WithOne().OnDelete(DeleteBehavior.Cascade);
               



        }
    }
}
