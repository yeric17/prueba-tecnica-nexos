using Application.Abstractions.Messaging;
using Domain.Orders.DTOs;

namespace Application.Orders.UpdateOrder
{
    public record UpdateOrderCommand : ICommand
    {
        public Guid OrderId { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
        public string Status { get; set; } = string.Empty;
        public string ShippingAddress { get; set; } = string.Empty;
        public string ShippingCity { get; set; } = string.Empty;
        public string ShippingCountry { get; set; } = string.Empty;
    }
}