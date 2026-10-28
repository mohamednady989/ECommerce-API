using ECommerce.Domain.Common;
using ECommerce.Domain.Contracts;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Repositres
{
    public class GenaricRepository<TEntity, TKey>(StoreDbContext dbContext) : IGenaricRepostiory<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public void Add(TEntity entity)
        => dbContext.Set<TEntity>().Add(entity);

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default)
            => await dbContext.Set<TEntity>().ToListAsync(ct);

     

        public async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct = default)
            => await dbContext.Set<TEntity>().FindAsync(id, ct);
            

       

        public void Remove(TEntity entity) =>dbContext.Set<TEntity>().Remove(entity);
        

        public void Upadte(TEntity entity)=> dbContext.Set<TEntity>().Update(entity);

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecification<TEntity, TKey> spec, CancellationToken ct = default)
        {
            var query = SpecificationEvaluator.CraeteQuery(dbContext.Set<TEntity>(), spec); 

            return await query.ToListAsync(ct);


        }

        public async Task<TEntity?> GetByIdAsync(ISpecification<TEntity, TKey> spec, CancellationToken ct = default)
        {
           var query=SpecificationEvaluator.CraeteQuery(dbContext.Set<TEntity>(), spec);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<int> CountAsync(ISpecification<TEntity, TKey> spec, CancellationToken ct = default)
        {
            return await SpecificationEvaluator.CraeteQuery(dbContext.Set<TEntity>(), spec).CountAsync(ct);
        }
    }
}
