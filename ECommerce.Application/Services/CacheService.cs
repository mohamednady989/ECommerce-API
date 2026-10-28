using ECommerce.Application.Contracts;
using ECommerce.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ECommerce.Application.Services
{
    public class CacheService : ICacheService
    {
        private readonly ICachRepository cachRepository;
        public CacheService(ICachRepository cachRepository)
        {
            this.cachRepository = cachRepository;
        }
        public async Task<string?> GetDAtaAsync(string cacheKey, CancellationToken ct = default)
        {
            return await cachRepository.GetAsync(cacheKey, ct);
        }

        public async Task SetDataAsync(string cacheKey, object cacheValue, TimeSpan? TimeToLive = null, CancellationToken ct = default)
        {
            var jesonValue = JsonSerializer.Serialize(cacheValue, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });
            await cachRepository.SetAsync(cacheKey, jesonValue, TimeToLive, ct);
        }
    }
}
