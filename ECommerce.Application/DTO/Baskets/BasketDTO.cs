using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.DTO.Baskets
{
    public  class BasketDTO
    {
        public string Id { get; set; } = default!;
        public ICollection<BasketItemDTO> Items { get; set; } = default!;
        public string? ClientSecret { get; set; }
        public string? PaymentIntenId { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal? ShippinfPrice { get; set; }
    }
}
