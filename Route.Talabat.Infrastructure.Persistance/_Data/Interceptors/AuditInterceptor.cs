using Microsoft.EntityFrameworkCore.Diagnostics;
using Route.Talabat.Application.Abstraction.Abstraction;
using Route.Talabat.Core.Domain.Common;

namespace Route.Talabat.Infrastructure.Persistance.Data.Interceptors
{
    public class AuditInterceptor :SaveChangesInterceptor
    {
        private readonly ILoggedUserService _loggedUserService;

        public AuditInterceptor(ILoggedUserService loggedUserService)
        {
            _loggedUserService = loggedUserService;
        }
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }


 
        private void UpdateEntities(DbContext ? dbcontext)
        {
            if (dbcontext is null) return;

            var utcNow= DateTime.UtcNow;
            var entries = dbcontext.ChangeTracker.Entries<IBaseAuditableEntity>();
            foreach (var entry in entries )
            {
                if (entry is { State :EntityState.Added or EntityState.Modified})
                {
                    if (entry.State==EntityState.Added)
                    {
                        entry.Entity.CreatedBy = _loggedUserService.UserId;
                        entry.Entity.CreatedOn = utcNow;
                    }

                    entry.Entity.LastModifiedBy =_loggedUserService.UserId;
                    entry.Entity.LastModifiedOn = utcNow;

                }
            }

        }
    }
}
