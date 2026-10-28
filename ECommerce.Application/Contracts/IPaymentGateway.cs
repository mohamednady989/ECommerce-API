using ECommerce.Application.Commen;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Contracts
{
    public  interface IPaymentGateway
    {
        Task<PaymentIntentResult> CreatePAymentIntentAsync(decimal amount, string currency,CancellationToken ct=default);
        Task<PaymentIntentResult> UpdatePAymentIntentAsync(decimal amount, string PaymentIntentId, CancellationToken ct = default);

    }
}
