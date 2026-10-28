using ECommerce.Application.Contracts;
using ECommerce.Application.DTO.Baskets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : APIBaseController
    {
        public IBasketService BasketService { get; }
        public BasketController(IBasketService basketService)
        {
            BasketService = basketService;
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<BasketDTO>> GetBasket(string Id, CancellationToken ct )
        {
            var result = await BasketService.GetBasketAsync(Id, ct);
            return ToActionResult(result);
        }
        [HttpPost]
        public async Task<ActionResult<BasketDTO>> CreateOrUpdateBasket(BasketDTO basket, CancellationToken ct )
        {
            var result = await BasketService.CreateOrUpdateBasketAsync(basket, ct: ct);
            return ToActionResult(result);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasket(string id, CancellationToken ct)
        {
            var result = await BasketService.DeleteBasketAsync(id, ct);
            return ToActionResult(result);
        }
    }
}
