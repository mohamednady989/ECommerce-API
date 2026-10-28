using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Contracts
{
    public interface ICachRepository
    {
        Task<string?> GetAsync(string cacheKey, CancellationToken ct = default);
        Task SetAsync(string cacheKey, string cacheValue, TimeSpan? timeToLive = default, CancellationToken ct = default);
    }
}
