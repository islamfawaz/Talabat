﻿using Route.Talabat.Core.Domain.Common;
using Route.Talabat.Core.Domain.Contract;
using Route.Talabat.Infrastructure.Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure.Persistance.Repositories
{
    internal class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
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

            if (withTracking) return await _dbContext.Set<TEntity>().ToListAsync();

            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetAsync(TKey id)
            => await _dbContext.Set<TEntity>().FindAsync(id);


        public async Task AddAsync(TEntity entity)
            => await _dbContext.Set<TEntity>().AddAsync(entity);


        public void Update(TEntity entity)
          => _dbContext.Set<TEntity>().Update(entity);



        public void Delete(TEntity entity)
            => _dbContext.Set<TEntity>().Remove(entity);



    }
}