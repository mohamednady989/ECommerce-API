using ECommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ECommerce.Domain.Contracts
{
    public  interface ISpecification<TEntity ,TKey> where TEntity : BaseEntity<TKey>
    {
        ICollection<Expression<Func<TEntity, object>>> IncludeExpression { get; }

        Expression<Func<TEntity, bool>> Criteria { get; }
        Expression<Func<TEntity, object>> OrderBy { get; }
        Expression<Func<TEntity, object>> OrderByDescending { get; }
        int Take { get; }
        int Skip { get; }
        bool IsPaginated { get; }
    }
}
