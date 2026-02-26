using Application.Abstractions.Messaging;
using Domain.Orders.DTOs;

namespace Application.Orders.GetOrdersByUserId
{
    public record GetOrdersByUserIdQuery : IQuery<List<OrderDto>>
    {
        public Guid UserId { get; set; }
    }
}
