﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.Talabat.Core.Domain.Entities.OrderAggregate;
using Route.Talabat.Infrastructure.Persistance.Data.Configuration.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure.Persistance._Data.Configuration.Orders
{
    internal class OrderItemConfigurations :BaseEntityConfigurations<OrderItem,int>
    {
        public override void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            base.Configure(builder);
            builder.OwnsOne(item => item.Product, product => product.WithOwner());

            builder.Property(item => item.Price).HasColumnType("decimal(2,8");
        }
    }
}