using Microsoft.EntityFrameworkCore;
using Route.Talabat.Core.Domain.Contract.Persistence.DbInitializer;
using Route.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure.Persistance.Common
{
    public abstract class DbInitializer : IDbInitializer
    {
        private readonly DbContext _dbcontext;

        public  DbInitializer(DbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public virtual async Task InitializerAsync()
        {
            var pendingMigration = _dbcontext.Database.GetPendingMigrations();
            if (pendingMigration.Any())
                await _dbcontext.Database.MigrateAsync();
        }

        public abstract  Task SeedAsnc();
       
    }
}
