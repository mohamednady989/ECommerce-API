using ECommerce.Application.Commen;
using ECommerce.Application.Contracts;
using Microsoft.Extensions.Options;
using Stripe;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Payment
{
    public class StripePaymentGateway : IPaymentGateway
    {
        private readonly PaymentGatewaySettints _payment;
        private readonly PaymentIntentService _paymentIntentService = new();


        public StripePaymentGateway(IOptions<PaymentGatewaySettints>options)
        {
            _payment = options.Value;
            StripeConfiguration.ApiKey = options.Value.SecretKey;
        }
        public async Task<PaymentIntentResult> CreatePAymentIntentAsync(decimal amount, string currency, CancellationToken ct = default)
        {
            var options = new PaymentIntentCreateOptions()
            {
                Amount = (long)amount,
                Currency = currency.ToLower(),
                AllowedPaymentMethodTypes = ["card"]

            };
            var intent=await _paymentIntentService.CreateAsync(options);
            return new PaymentIntentResult(intent.Id, intent.ClientSecret);
        }

        public async Task<PaymentIntentResult> UpdatePAymentIntentAsync(decimal amount, string PaymentIntentId, CancellationToken ct = default)
        {
        var options=new PaymentIntentUpdateOptions()
        {
            Amount=(long) amount
        };
            var intent = await _paymentIntentService.UpdateAsync(PaymentIntentId,options);
            return new PaymentIntentResult(intent.Id, intent.ClientSecret);

        }
    }
}
