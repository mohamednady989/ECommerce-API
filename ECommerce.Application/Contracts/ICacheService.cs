using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Contracts
{
    public interface ICacheService
    {
        Task<string?>GetDAtaAsync(string cacheKey, CancellationToken ct = default);
        Task SetDataAsync(string cacheKey, object cacheValue, TimeSpan? TimeToLive = default, CancellationToken ct = default);
    }
}
