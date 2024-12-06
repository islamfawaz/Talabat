using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.Talabat.Core.Domain.Entities.OrderAggregate;
using Route.Talabat.Infrastructure.Persistance._Data.Configuration.Base;

internal class OrderConfigurations : BaseAuditableEntityConfigurations<Order, int>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);

     

        builder.Property(order => order.Status)
            .HasConversion(
                oStatus => oStatus.ToString(),
                oStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), oStatus));

        builder.Property(order => order.Subtotal).HasColumnType("decimal(8,2)");

        builder.HasOne(order => order.DeliveryMethod)
            .WithMany()
            .HasForeignKey(order => order.DeliveryMethodId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(order => order.Items)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
