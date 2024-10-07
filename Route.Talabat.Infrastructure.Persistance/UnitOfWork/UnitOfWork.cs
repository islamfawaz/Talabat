using Route.Talabat.Core.Domain.Contract;
using Route.Talabat.Core.Domain.Entities.Products;
using Route.Talabat.Infrastructure.Persistance.Data;
using Route.Talabat.Infrastructure.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure.Persistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {


        #region Services

        private readonly StoreContext _dbContext;
        private readonly  Lazy<IGenericRepository<Product, int>> _productRepository;
        private readonly  Lazy<IGenericRepository<ProductBrand, int>> _brandsRepository;
        private readonly  Lazy<IGenericRepository<ProductCategory, int>> _categoryRepository;

        public UnitOfWork(StoreContext dbContext)
        {
            _dbContext = dbContext;
            _productRepository = new Lazy<IGenericRepository<Product, int>>( ()=> new GenericRepository<Product,int>(_dbContext) );
            _brandsRepository = new Lazy<IGenericRepository<ProductBrand, int>>( ()=> new GenericRepository<ProductBrand,int>(_dbContext) );
            _categoryRepository = new Lazy<IGenericRepository<ProductCategory, int>>( ()=> new GenericRepository<ProductCategory,int>(_dbContext) );
        } 

        #endregion


        public IGenericRepository<Product, int> ProductRepository => _productRepository.Value;
        public IGenericRepository<ProductBrand, int> BrandsRepository => _brandsRepository.Value;
        public IGenericRepository<ProductCategory, int> CategoryRepository => _categoryRepository.Value;



        public Task<int> CompleteAsync()
        {
            throw new NotImplementedException();
        }

        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }



    }
}
