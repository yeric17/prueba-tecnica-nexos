using FluentValidation;


namespace Application.Orders.CreateOrder
{
    public sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
            RuleFor(x => x.Items).NotEmpty().WithMessage("At least one order item is required.");
            RuleForEach(x => x.Items).SetValidator(new OrderItemDtoValidator());
            RuleFor(x => x.ShippingAddress).NotEmpty().WithMessage("ShippingAddress is required.");
            RuleFor(x => x.ShippingCity).NotEmpty().WithMessage("ShippingCity is required.");
            RuleFor(x => x.ShippingCountry).NotEmpty().WithMessage("ShippingCountry is required.");
        }
    }
}
