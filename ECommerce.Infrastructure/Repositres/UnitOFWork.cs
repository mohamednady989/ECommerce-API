using ECommerce.Domain.Common;
using ECommerce.Domain.Contracts;
using ECommerce.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Repositres
{
    public class UnitOFWork(StoreDbContext dbContext) : IUnitOfWork
    {

        private readonly Dictionary<string, object> repositories = [];
        public IGenaricRepostiory<TEntity, TKey> GetRepostiory<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var typeName=typeof(TEntity).Name;
            if (repositories.TryGetValue(typeName, out object? value))
                return (IGenaricRepostiory<TEntity,TKey>) value;
            else
            {
                var NewRepo = new GenaricRepository<TEntity, TKey>(dbContext);
                repositories[typeName] = NewRepo;
                return NewRepo;

            }
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        
           => await dbContext.SaveChangesAsync(ct);


        
    }
}
