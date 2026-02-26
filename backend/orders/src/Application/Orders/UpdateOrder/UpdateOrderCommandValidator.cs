using Application.Orders.CreateOrder;
using FluentValidation;

namespace Application.Orders.UpdateOrder
{
    public sealed class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(command => command.OrderId).NotEmpty().WithMessage("OrderId is required.");
            RuleFor(command => command.Items).NotEmpty().WithMessage("At least one order item is required.");
            RuleForEach(command => command.Items).SetValidator(new OrderItemDtoValidator());
            RuleFor(command => command.Status).NotEmpty().WithMessage("Status is required.");
            RuleFor(command => command.ShippingAddress).NotEmpty().WithMessage("ShippingAddress is required.");
            RuleFor(command => command.ShippingCity).NotEmpty().WithMessage("ShippingCity is required.");
            RuleFor(command => command.ShippingCountry).NotEmpty().WithMessage("ShippingCountry is required.");
        }
    }
}