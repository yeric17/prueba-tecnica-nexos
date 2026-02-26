namespace Domain.Orders.DTOs
{
    public class UpdateOrderDto
    {
        public List<OrderItemDto> Items { get; set; } = new();
        public string Status { get; set; } = string.Empty;
        public string? ShippingAddress { get; set; }
        public string? ShippingCity { get; set; }
        public string? ShippingCountry { get; set; }
    }
}