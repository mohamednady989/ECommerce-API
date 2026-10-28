namespace ECommerce.Application.DTO.Order
{
    public class OrderItemDTO
    {
        public int ProductId { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        public decimal Price  { get; set; }
        public int Quantity { get; set; }
        public object ProductName { get;set; } = default!;
    }
}