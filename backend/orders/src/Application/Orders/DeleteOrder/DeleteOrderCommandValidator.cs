using FluentValidation;

namespace Application.Orders.DeleteOrder
{
    public sealed class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator()
        {
            RuleFor(command => command.OrderId).NotEmpty().WithMessage("OrderId is required.");
        }
    }
}