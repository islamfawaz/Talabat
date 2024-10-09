using Route.Talabat.Core.Domain.Common;
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
            if (typeof(TEntity) == typeof(Product))
                return withTracking ? (IEnumerable<TEntity>)await _dbContext.Set<Product>().Include(p => p.Brand).Include(p => p.Category).ToListAsync()
              : (IEnumerable<TEntity>)await _dbContext.Set<Product>().Include(p => p.Brand).Include(p => p.Category).AsNoTracking().ToListAsync();


            else
            {

                if (withTracking)

                    return await _dbContext.Set<TEntity>().ToListAsync();

                return await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
            }


        }

        public async Task<TEntity?> GetAsync(TKey id)
        {
            if (typeof(TEntity)==typeof(Product))
            
               return await _dbContext.Set<Product>().Where(P => P.Id.Equals(id)).Include(P=>P.Brand).Include(P=>P.Category).FirstOrDefaultAsync() as TEntity;

            return  await _dbContext.Set<TEntity>().FindAsync(id);

        }




        public async Task AddAsync(TEntity entity)
            => await _dbContext.Set<TEntity>().AddAsync(entity);


        public void Update(TEntity entity)
          => _dbContext.Set<TEntity>().Update(entity);



        public void Delete(TEntity entity)
            => _dbContext.Set<TEntity>().Remove(entity);



    }
}
