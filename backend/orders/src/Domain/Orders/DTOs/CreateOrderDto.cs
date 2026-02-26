using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Orders.DTOs
{
    public class CreateOrderDto
    {
        public string UserId { get; set; } = string.Empty;
        public List<OrderItemDto> Items { get; set; } = new();
        public string? ShippingAddress { get; set; }
        public string? ShippingCity { get; set; }
        public string? ShippingCountry { get; set; }
    }
}
