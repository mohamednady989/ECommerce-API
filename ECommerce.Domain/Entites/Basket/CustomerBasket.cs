using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entites.Basket
{
    public  class CustomerBasket
    {
        public string Id { get; set; } = default!;
        public ICollection<BasketItem> Items { get; set; } = [];
        public string? ClientSecret { get; set; }
        public string? PaymentIntenId { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal? ShippinfPrice { get; set; }

    }
}
