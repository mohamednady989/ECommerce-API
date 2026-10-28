using ECommerce.Application.Contracts;
using ECommerce.Application.DTO.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class OrderController: APIBaseController
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService )
        {
            this.orderService = orderService;
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDTO>> CreateOrder(OrderDTO orderDTO, CancellationToken ct)
        {
            return ToActionResult(await orderService.CreateOrdedrAsync(orderDTO, GetEmailFromToken(), ct));
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDTO>>> GetAllOrders(CancellationToken ct)
        {
            return ToActionResult(await orderService.GetAllOrderForUserAsync(GetEmailFromToken(), ct));
        }
        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderToReturnDTO>> GetOrderById(Guid id, CancellationToken ct)
        {
            return ToActionResult(await orderService.GetOrderByIdAndEmailFromUserAsync(id, GetEmailFromToken(), ct));
            
        }
        //Get:: BaseURL/api/Order/DeliveryMethod
        [AllowAnonymous]//Anyone can Show this method
        [HttpGet("DeliveryMethod")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethodDTO>>> GetDeliveryMethods(CancellationToken ct )
        {
            return ToActionResult(await orderService.GetDeliveryMethedsAsync(ct));
        }

    }
}
