using ECommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Contracts
{
    public  interface IGenaricRepostiory<TEntity , TKey >where TEntity: BaseEntity<TKey>
    {
        void Add(TEntity entity);
        void Upadte(TEntity entity);
        void Remove (TEntity entity);
        Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct = default);
        Task<TEntity?> GetByIdAsync(ISpecification<TEntity,TKey> spec, CancellationToken ct = default);

        Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default);
        Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecification<TEntity, TKey> spec, CancellationToken ct = default);
        Task <int >CountAsync(ISpecification<TEntity, TKey> spec, CancellationToken ct = default);






    }
}
