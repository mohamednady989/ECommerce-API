using ECommerce.Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Repositres
{
    public class CachRepository : ICachRepository
    {
        private readonly IDatabase database;
        public CachRepository(IConnectionMultiplexer connection)
        {
            database = connection.GetDatabase();
        }
        public async Task<string?> GetAsync(string cacheKey, CancellationToken ct = default)
        {
            var value =await database.StringGetAsync(cacheKey);
            return value.IsNullOrEmpty ? null : value.ToString();
        }

        public async Task SetAsync(string cacheKey, string cacheValue, TimeSpan? timeToLive = null, CancellationToken ct = default)
        {
            await database.StringSetAsync(cacheKey, cacheValue, timeToLive?? TimeSpan.FromMinutes(2));
        }
    }
}
