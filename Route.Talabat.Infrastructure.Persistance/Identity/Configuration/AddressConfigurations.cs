﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.Talabat.Core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure.Persistance.Identity.Configuration
{
    public class AddressConfigurations : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {;
            builder.ToTable("Adresses");
        }
    }
}
