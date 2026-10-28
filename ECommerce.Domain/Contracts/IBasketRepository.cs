using ECommerce.Domain.Entites.Basket;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Contracts
{
    public  interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string basketId, CancellationToken ct = default);
        Task<CustomerBasket?> CraeteOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = default ,CancellationToken ct = default);
        Task<bool> DeleteBasketAsync(string basketId, CancellationToken ct = default);

    }
}
