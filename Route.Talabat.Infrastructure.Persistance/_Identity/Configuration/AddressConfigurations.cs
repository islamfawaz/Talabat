using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.Talabat.Core.Domain.Entities.Identity;
using Route.Talabat.Infrastructure.Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure.Persistance.Identity.Configuration
{
    [DbContext(typeof(StoreIdentityDbContext))]

    public class AddressConfigurations : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Adresses");
            builder.Property(nameof(Address.Id)).ValueGeneratedOnAdd();
            builder.Property(nameof(Address.FName)).HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(nameof(Address.LName)).HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(nameof(Address.Street)).HasColumnType("varchar").HasMaxLength(50);

            builder.Property(nameof(Address.Country)).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(nameof(Address.City)).HasColumnType("varchar").HasMaxLength(50);


        }
    }
}
