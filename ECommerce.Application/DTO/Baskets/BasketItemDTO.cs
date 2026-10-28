using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerce.Application.DTO.Baskets
{
    public  class BasketItemDTO
    {
        [Required(ErrorMessage = "Product ID is required")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Product name is required")]
        public string ProductName { get; set; } = default!;
        [Required(ErrorMessage = "Picture URL is required")]
        public string PictureUrl { get; set; } = default!;
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
    }
}
