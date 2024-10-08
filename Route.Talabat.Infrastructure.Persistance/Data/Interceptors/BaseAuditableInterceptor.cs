using Microsoft.EntityFrameworkCore.Diagnostics;
using Route.Talabat.Application.Abstraction.Abstraction;
using Route.Talabat.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure.Persistance.Data.Interceptors
{
    public class BaseAuditableInterceptor :SaveChangesInterceptor
    {
        private readonly ILoggedUserService _loggedUserService;

        public BaseAuditableInterceptor(ILoggedUserService loggedUserService)
        {
            _loggedUserService = loggedUserService;
        }
        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }
        private void UpdateEntities(DbContext ? dbcontext)
        {
            if (dbcontext is null) return;

            var utcNow= DateTime.UtcNow;
            foreach (var entry in dbcontext.ChangeTracker.Entries<BaseAuditableEntity<int>>())
            {
                if (entry is { State :EntityState.Added or EntityState.Modified})
                {
                    if (entry.State==EntityState.Added)
                    {
                        entry.Entity.CreatedBy = "";
                        entry.Entity.CreatedOn = utcNow;
                    }
                    entry.Entity.LastModifiedBy = "";
                    entry.Entity.LastModifiedOn = utcNow;

                }
            }

        }
    }
}
