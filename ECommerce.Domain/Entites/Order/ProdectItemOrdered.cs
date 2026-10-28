using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entites.Order
{
    public  class ProdectItemOrdered
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public string ProductUrl { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
    }
}
