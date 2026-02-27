namespace Domain.Orders.DTOs
{
    public class OrderWithUserDto
    {
        public Guid Id { get; set; }
        public Guid OrderNumber { get; set; }
        public Guid UserId { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
        public string? ShippingAddress { get; set; }
        public string? ShippingCity { get; set; }
        public string? ShippingCountry { get; set; }

        public static OrderWithUserDto FromOrder(Order order, string userEmail = "", string userName = "")
        {
            var dto = new OrderWithUserDto
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                UserId = order.UserId,
                UserEmail = userEmail,
                UserName = userName,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                ShippingAddress = order.ShippingAddress,
                ShippingCity = order.ShippingCity,
                ShippingCountry = order.ShippingCountry,
                Items = order.Items.Select(OrderItemDto.FromOrderItem).ToList()
            };
            
            return dto;
        }
    }
}
