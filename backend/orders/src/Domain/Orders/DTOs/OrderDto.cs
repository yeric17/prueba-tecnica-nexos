namespace Domain.Orders.DTOs
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid OrderNumber { get; set; }
        public string UserId { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
        public string? ShippingAddress { get; set; }


        public static OrderDto FromOrder(Order order)
        {
            var dto = new OrderDto
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                UserId = order.UserId,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                ShippingAddress = order.ShippingAddress,
                Items = order.Items.Select(OrderItemDto.FromOrderItem).ToList()
            };
            
            return dto;
        }
    }
}
