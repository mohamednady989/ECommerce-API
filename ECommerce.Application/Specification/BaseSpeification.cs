using ECommerce.Domain.Common;
using ECommerce.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ECommerce.Application.Specification
{
    public abstract class BaseSpeification<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public ICollection<Expression<Func<TEntity, object>>> IncludeExpression { get; } = [];

        public Expression<Func<TEntity, bool>> Criteria { get; private set; }
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }
        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPaginated { get; private set; }

        protected void AddInclude(Expression<Func<TEntity, object>> include)
        {
            IncludeExpression.Add(include);

        }
        protected BaseSpeification(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }


        protected void AddorderBy(Expression<Func<TEntity, object>> OrderByExpression)
        {
            OrderBy = OrderByExpression;
        }

        protected void AddorderByDescending(Expression<Func<TEntity, object>> OrderByDescendingExpression)
        {
            OrderByDescending = OrderByDescendingExpression;
        }

        protected void ApplyPagination(int pageSize, int PageIndex)
        {
            IsPaginated = true;
            Take = pageSize;
            Skip = pageSize * (PageIndex - 1);
        }




    }

}
