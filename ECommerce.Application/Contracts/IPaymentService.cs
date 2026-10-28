using ECommerce.Application.Commen;
using ECommerce.Application.DTO.Baskets;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Contracts
{
    public  interface IPaymentService
    {
        Task<Result<BasketDTO>> CreateOrUpdatePaymentIntentAsync(string basketId,CancellationToken ct=default);
    }
}
