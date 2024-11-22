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
            builder.Property(O => O.Status).
                HasConversion(
                 OStatus => OStatus.ToString()
                //  OStatus => Enum.Parse(typeof(OrderStatus), OStatus.ToString())

                );
        }
    }
}
