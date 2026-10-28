using ECommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entites.Order
{
    public  class OrderItem:BaseEntity<int>
    {
        public ProdectItemOrdered Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
