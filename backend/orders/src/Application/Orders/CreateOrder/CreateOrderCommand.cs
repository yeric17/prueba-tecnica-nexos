using Application.Abstractions.Messaging;
using Domain.Orders.DTOs;


namespace Application.Orders.CreateOrder
{
    public record CreateOrderCommand : ICommand<Guid>
    {
        public string UserId { get; set; } = string.Empty;
        public List<OrderItemDto> Items { get; set; } = new();
        public string ShippingAddress { get; set; } = string.Empty;
        public string ShippingCity { get; set; } = string.Empty;
        public string ShippingCountry { get; set; } = string.Empty;
    }
}
