using ECommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Contracts
{
    public  interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken ct = default);
        IGenaricRepostiory<TEntity, TKey> GetRepostiory<TEntity, TKey>() where TEntity : BaseEntity<TKey>;

    }
}
