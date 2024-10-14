using Route.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Domain.Contract
{
    public interface ISpecifications<TEntity ,TKey> where TEntity : BaseEntity<TKey>where TKey : IEquatable<TKey>
    {
        public Expression<Func<TEntity ,bool>> Criteria { get; set; }//P=>P.Id

        public List<Expression<Func<TEntity,object>>> Includes { get; set; }

        public Expression<Func<TEntity,object>> ? OrderBy { get; set; }
        public Expression<Func<TEntity, object>> ? OrderByDesc { get; set; }

        public int Skip { get; set; }
        public int Take { get; set; }

        public bool IsPaginate { get; set; }



    }
}
