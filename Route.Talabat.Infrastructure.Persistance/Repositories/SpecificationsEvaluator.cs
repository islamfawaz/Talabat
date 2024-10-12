﻿using Route.Talabat.Core.Domain.Common;
using Route.Talabat.Core.Domain.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure.Persistance.Repositories
{
    internal class SpecificationsEvaluator<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inptuQuery, ISpecifications<TEntity, TKey> spec)
        {
            var query = inptuQuery;

            if (spec.Criteria is not null)
                query = query.Where(spec.Criteria);



            if (spec.OrderByDesc is not null)
            {
                query = query.OrderByDescending(spec.OrderByDesc);   
            }


           else if (spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            //include Exepression


            query = spec.Includes.Aggregate(query, (currentQuery, icludeExpression) => currentQuery.Include(icludeExpression));


            return query;
        }
    }
}
