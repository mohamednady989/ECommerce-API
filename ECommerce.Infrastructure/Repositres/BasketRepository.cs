using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entites.Basket;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ECommerce.Infrastructure.Repositres
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase database;
        public BasketRepository(IConnectionMultiplexer connection)
        {
            database = connection.GetDatabase();
        }
        public async Task<CustomerBasket?> CraeteOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null, CancellationToken ct = default)
        {
           var value=JsonSerializer.Serialize(basket);
            var result=await database.StringSetAsync(basket.Id, value, timeToLive ?? TimeSpan.FromDays(7));
            return result ? basket : null;
        }

        public async Task<bool> DeleteBasketAsync(string basketId, CancellationToken ct = default)
        {
            return await database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId, CancellationToken ct = default)
        {
           var basket=await database.StringGetAsync(basketId);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>((string)basket);
        }
    }
}
