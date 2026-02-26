using Domain.Orders.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Orders
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string UserId { get; set; } = string.Empty;

        public Guid OrderNumber { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Completed, Cancelled
        public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }

        public string? ShippingAddress { get; set; }
        public string? ShippingCity { get; set; }
        public string? ShippingCountry { get; set; }

        #region Navigation Properties
        public List<OrderItem> Items { get; set; } = new();
        #endregion

        public decimal TotalAmount
        {
            get
            {
                decimal total = 0;
                foreach (var item in Items)
                {
                    total += item.Subtotal;
                }
                return total;
            }
        }

        private Order() { }

        public Order(CreateOrderDto dto)
        {
            UserId = dto.UserId;
            ShippingAddress = dto.ShippingAddress;
            ShippingCity = dto.ShippingCity;
            ShippingCountry = dto.ShippingCountry;
            OrderNumber = Guid.NewGuid();

            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

    }
}
