using Domain.Orders.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Orders
{
    public class Order
    {
        public Guid Id { get; private set; }

        public Guid UserId { get; private set; }

        public Guid OrderNumber { get; private set; }
        public string Status { get; private set; } = "Pending"; // Pending, Completed, Cancelled
        public DateTimeOffset CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTimeOffset? UpdatedAt { get; private set; }

        public string? ShippingAddress { get; private set; }
        public string? ShippingCity { get; private set; }
        public string? ShippingCountry { get; private set; }

        #region Navigation Properties
        public List<OrderItem> Items { get; private set; } = new();
        #endregion

        public decimal TotalAmount { get; private set; }

        private Order() { }

        public Order(CreateOrderDto dto)
        {
            UserId = dto.UserId;
            ShippingAddress = dto.ShippingAddress;
            ShippingCity = dto.ShippingCity;
            ShippingCountry = dto.ShippingCountry;
            OrderNumber = Guid.NewGuid();
            Items = dto.Items
                .Select(item => new OrderItem(Id, item.ProductName, item.Quantity, item.UnitPrice))
                .ToList();

            TotalAmount = Items.Sum(i => i.Subtotal);

            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void Update(UpdateOrderDto dto)
        {
            ShippingAddress = dto.ShippingAddress;
            ShippingCity = dto.ShippingCity;
            ShippingCountry = dto.ShippingCountry;
            Status = dto.Status;
            UpdatedAt = DateTimeOffset.UtcNow;


            Items = dto.Items
                .Select(item => new OrderItem(Id, item.ProductName, item.Quantity, item.UnitPrice))
                .ToList();

            TotalAmount = Items.Sum(i => i.Subtotal);
        }

    }
}
