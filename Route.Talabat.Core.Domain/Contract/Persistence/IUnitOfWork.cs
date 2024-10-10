using Route.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Domain.Contract.Persistence
{
    public interface IUnitOfWork : IAsyncDisposable

    {

        //public IGenericRepository<Product , int > ProductRepository { get; }
        //public IGenericRepository<ProductBrand , int > BrandsRepository { get; }
        //public IGenericRepository<ProductCategory , int > CategoryRepository { get; }


        IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseAuditableEntity<TKey>
            where TKey : IEquatable<TKey>;



        Task<int> CompleteAsync();
    }
}
