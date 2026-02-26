using Application.Abstractions.Messaging;

namespace Application.Orders.DeleteOrder
{
    public record DeleteOrderCommand : ICommand
    {
        public Guid OrderId { get; set; }
    }
}