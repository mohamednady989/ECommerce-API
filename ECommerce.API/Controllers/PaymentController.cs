using ECommerce.Application.Contracts;
using ECommerce.Application.DTO.Baskets;
using ECommerce.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class PaymentController:APIBaseController
    {
        private readonly IPaymentService paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }
        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketDTO>> CreateOrUpdatePaymentIntent(string basketId, CancellationToken ct = default)
        {
            var result = await paymentService.CreateOrUpdatePaymentIntentAsync(basketId, ct);
            return ToActionResult(result);
        }
    }
}
