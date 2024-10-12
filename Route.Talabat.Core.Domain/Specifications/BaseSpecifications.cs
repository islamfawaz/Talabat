using Route.Talabat.Core.Domain.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Domain.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey>
        where TEntity : BaseEntity<TKey> 
        where TKey : IEquatable<TKey>
    {
        public Expression<Func<TEntity, bool>> Criteria { get; set; } = null!; 
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new ();
        public Expression<Func<TEntity, object>> ? OrderBy { get; set; }
        public Expression<Func<TEntity, object>> ? OrderByDesc { get; set; }

        public BaseSpecifications(Expression<Func<TEntity, bool>> criteriaExpression)
        {
            Criteria = criteriaExpression;
         //   Includes = new List<Expression<Func<TEntity, object>>>();
        }

        public BaseSpecifications(TKey id)
        {
            Criteria = E => E.Id.Equals(id);

            // Includes = new List<Expression<Func<TEntity, object>>>();
        }


        protected private void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        protected BaseSpecifications()
        {
            
        }

        protected private void AddOrderByDesc(Expression<Func<TEntity, object>> orderByExpressionDesc)
        {
            OrderByDesc = orderByExpressionDesc;
        }

    

        protected private virtual void  AddInclude()
        {
           
        }
    }
}
