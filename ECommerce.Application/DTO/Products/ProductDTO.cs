using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.DTO.Products
{
    public  class ProductDTO
    {
        public string ProductBrand { get; set; } = default!;

        public int    Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        public string ProductType { get; set; } = default!;
        public decimal Price { get; set; }




    }
}
