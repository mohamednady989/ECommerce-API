using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerce.Application.DTO.Order
{
    public  class OrderDTO
    {
        [Required]
        public string BasketId { get; set; } = default!;
        [Required]
        public int DeliveryMethodId { get; set; }
        [Required]

        public AddressDTO ShipTpAddress { get; set; } = default!;



    }
}
