using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.Talabat.Core.Domain.Entities.OrderAggregate;
using Route.Talabat.Infrastructure.Persistance.Data.Configuration.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure.Persistance._Data.Configuration.Orders
{
    internal class DeliveryMethodConfigurations :BaseEntityConfigurations<DeliveryMethod,int>
    {
        public override void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            base.Configure(builder);
            builder.Property(D => D.Cost).HasColumnType("decimal(2,8)");
        }

    }
}
