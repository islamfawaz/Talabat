using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.Talabat.Core.Domain.Common;
using Route.Talabat.Infrastructure.Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure.Persistance._Data.Configuration.Base
{
    [DbContext(typeof(StoreDbContext))]
    public class BaseAuditableEntityConfigurations<TEntity, TKey> : IEntityTypeConfiguration<TEntity> where
       TEntity : BaseAuditableEntity<TKey> where TKey : IEquatable<TKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            //builder.Property(E => E.Id).UseIdentityColumn(1, 1);


            builder.Property(E => E.CreatedOn).IsRequired();

            builder.Property(E => E.CreatedBy).IsRequired();

            builder.Property(E => E.LastModifiedBy).IsRequired();

            builder.Property(E => E.LastModifiedOn).IsRequired();

        }
    }
}
