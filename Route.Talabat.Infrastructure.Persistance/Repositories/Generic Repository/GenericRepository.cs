using Route.Talabat.Core.Domain.Common;
using Route.Talabat.Core.Domain.Contract;
using Route.Talabat.Core.Domain.Contract.Persistence;
using Route.Talabat.Core.Domain.Entities.Products;
using Route.Talabat.Infrastructure.Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure.Persistance.Repositories
{
    internal class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
        where TEntity : BaseAuditableEntity<TKey>
        where TKey : IEquatable<TKey>
    {


        #region Services

        private readonly StoreContext _dbContext;

        public GenericRepository(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion



        public async Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false)
        {




            return withTracking ?
                     await _dbContext.Set<TEntity>().ToListAsync() :
                     await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();


        }

        public async Task<TEntity?> GetAsync(TKey id)
        {
            return  await _dbContext.Set<TEntity>().FindAsync(id);

        }




        public async Task AddAsync(TEntity entity)
            => await _dbContext.Set<TEntity>().AddAsync(entity);


        public void Update(TEntity entity)
          => _dbContext.Set<TEntity>().Update(entity);
        public async Task<int> GetCountAsync(ISpecifications<TEntity, TKey> spec)
        {
            return await ApplySpecifications(spec).CountAsync();
        }


        public void Delete(TEntity entity)
            => _dbContext.Set<TEntity>().Remove(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsyncWithSpec(ISpecifications<TEntity, TKey> spec, bool withTracking = false)
        {
            return await ApplySpecifications(spec).ToListAsync();
        }

        public async Task<TEntity?> GetAsyncWithSpec(ISpecifications<TEntity, TKey> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();
        }

        #region Helper 
        private IQueryable<TEntity> ApplySpecifications(ISpecifications<TEntity, TKey> spec, bool withTracking = false)
        {
            var query = SpecificationsEvaluator<TEntity, TKey>.GetQuery(_dbContext.Set<TEntity>(), spec);
            return withTracking ? query : query.AsNoTracking();
        }

      

        #endregion

    }
}
