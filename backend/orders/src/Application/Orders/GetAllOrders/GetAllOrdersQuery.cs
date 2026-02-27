using Application.Abstractions.Messaging;
using Domain.Orders.DTOs;

namespace Application.Orders.GetAllOrders
{
    public record GetAllOrdersQuery : IQuery<List<OrderDto>>
    {
    }
}
