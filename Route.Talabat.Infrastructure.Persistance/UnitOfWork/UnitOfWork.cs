using Route.Talabat.Core.Domain.Common;
using Route.Talabat.Core.Domain.Contract.Persistence;
using Route.Talabat.Infrastructure.Persistance.Data;
using Route.Talabat.Infrastructure.Persistance.Generic_Repositories;
using System.Collections.Concurrent;

namespace Route.Talabat.Infrastructure.Persistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {


        #region Services

        private readonly StoreDbContext _dbContext;
        private readonly ConcurrentDictionary<string , object> _repositories;

        ///private readonly  Lazy<IGenericRepository<ProductCategory, int>> _categoryRepository;
        ///private readonly  Lazy<IGenericRepository<ProductBrand, int>> _brandsRepository;
        ///private readonly  Lazy<IGenericRepository<Product, int>> _productRepository;

        public UnitOfWork(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new ConcurrentDictionary<string , object>();

            ///_productRepository = new Lazy<IGenericRepository<Product, int>>( ()=> new GenericRepository<Product,int>(_dbContext) );
            ///_brandsRepository = new Lazy<IGenericRepository<ProductBrand, int>>( ()=> new GenericRepository<ProductBrand,int>(_dbContext) );
            ///_categoryRepository = new Lazy<IGenericRepository<ProductCategory, int>>( ()=> new GenericRepository<ProductCategory,int>(_dbContext) );
        }

        #endregion


        //public IGenericRepository<Product, int> ProductRepository => _productRepository.Value;
        //public IGenericRepository<ProductBrand, int> BrandsRepository => _brandsRepository.Value;
        //public IGenericRepository<ProductCategory, int> CategoryRepository => _categoryRepository.Value;


        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseAuditableEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            ///var typeName = typeof(TEntity).Name; // Product 
            ///
            ///if( _repositories.ContainsKey(typeName))
            ///    return (IGenericRepository<TEntity , TKey>) _repositories[typeName];
            ///
            ///var repository = new GenericRepository<TEntity, TKey>(_dbContext);
            ///_repositories.Add(typeName, repository);
            ///
            ///return repository;



            return (IGenericRepository<TEntity, TKey>)_repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity, TKey>(_dbContext));

        }


          
        public async Task<int> CompleteAsync() => await _dbContext.SaveChangesAsync();

        public ValueTask DisposeAsync() => _dbContext.DisposeAsync();





    }
}
