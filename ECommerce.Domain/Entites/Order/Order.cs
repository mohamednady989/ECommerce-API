using ECommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entites.Order
{
    public  class Order:BaseEntity<Guid>
    {
      
        public Order(string buyerEmail,OrderAddress shipToAddress,ICollection<OrderItem> items,DeliveryMethod deliveryMethod,decimal Subtotal   )
        {
            BuyerEmail = buyerEmail;
            ShippingAdderss = shipToAddress;
            Items = items;
            DeliveryMethod = deliveryMethod;
            subtotal = Subtotal;
            
        }

        public string BuyerEmail { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public OrderAddress ShippingAdderss { get; set; } = default!;
        public ICollection<OrderItem> Items { get; set; } = [];
        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public decimal subtotal { get; set; }
        public decimal Total { get; set; }

        public decimal GetTotal => subtotal + (DeliveryMethod?.Price ?? 0);



    }
}
