using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ECommerce.Application.DTO.Order
{
    public class OrderToReturnDTO
    {
        public Guid Id { get; set; }
        public string BuyerEmail { get; set; } = default!;
        public DataSetDateTime OrderData { get; set; }
        public AddressDTO ShipToAdderss { get; set; } = default!;
        public string Statuds { get; set; } = default!;
        public string DeliveryMethod { get; set; } = default!;
        public decimal Total { get; set; }
        public decimal SubTotal { get; set; }
        public decimal DeliveryMethodCost { get; set; }
        public ICollection<OrderItemDTO>Items { get; set; }


    }
}
