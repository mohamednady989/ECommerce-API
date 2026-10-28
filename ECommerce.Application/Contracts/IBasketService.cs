using ECommerce.Application.Commen;
using ECommerce.Application.DTO.Baskets;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Contracts
{
    public  interface IBasketService
    {
        Task <Result<BasketDTO>>GetBasketAsync(string basketId ,CancellationToken ct = default);
        Task<Result<BasketDTO>> CreateOrUpdateBasketAsync(BasketDTO basket,TimeSpan? TLV=default, CancellationToken ct = default);
        Task<Result<bool>> DeleteBasketAsync(string basketId, CancellationToken ct = default);

    }
}
