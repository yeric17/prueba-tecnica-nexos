using Application.Abstractions.Messaging;
using Domain.Orders.DTOs;

namespace Application.Orders.GetUserProducts
{
    public record GetUserProductsQuery : IQuery<List<OrderItemDto>>
    {
        public Guid UserId { get; set; } 
    }
}