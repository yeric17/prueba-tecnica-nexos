using Application.Abstractions.Messaging;
using Domain.Orders.DTOs;

namespace Application.Orders.GetOrderById
{
    public record GetOrderByIdQuery : IQuery<OrderDto>
    {
        public Guid OrderId { get; set; }
    }
}
