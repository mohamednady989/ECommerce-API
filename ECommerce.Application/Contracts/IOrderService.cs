using ECommerce.Application.Commen;
using ECommerce.Application.DTO.Order;
using ECommerce.Domain.Entites.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Application.Contracts
{
    public interface IOrderService
    {
        Task<Result<OrderToReturnDTO>> CreateOrdedrAsync(OrderDTO order, string email, CancellationToken ct = default);
        Task<Result<IReadOnlyList<OrderToReturnDTO>>> GetAllOrderForUserAsync(string email, CancellationToken ct = default);
        Task<Result<OrderToReturnDTO>> GetOrderByIdAndEmailFromUserAsync(Guid id,string emali ,CancellationToken ct=default);
        Task<Result<IReadOnlyList<DeliveryMethodDTO>>> GetDeliveryMethedsAsync(CancellationToken ct=default);

    }
}
