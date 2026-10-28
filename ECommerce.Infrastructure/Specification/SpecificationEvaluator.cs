using ECommerce.Domain.Common;
using ECommerce.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Specification
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CraeteQuery<TEntity, TKey>(IQueryable<TEntity> inputQuret, ISpecification<TEntity, TKey> spec) where TEntity : BaseEntity<TKey>
        {
            var quruy = inputQuret;
            if (spec.IncludeExpression.Any())
            {
                foreach (var expression in spec.IncludeExpression)
                {
                    quruy = quruy.Include(expression);
                }
            }

            if (spec.Criteria != null)
            {
                quruy = quruy.Where(spec.Criteria);
            }
            if(spec.OrderBy != null)
            {
                quruy = quruy.OrderBy(spec.OrderBy);
            }
            if (spec.OrderByDescending != null)
            {
               quruy = quruy.OrderByDescending(spec.OrderByDescending);
            }
            if (spec.IsPaginated)
            {
                quruy = quruy.Skip(spec.Skip).Take(spec.Take);
            }

            return quruy;
        }
    }
}

           
