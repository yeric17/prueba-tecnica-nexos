using Domain.Orders.DTOs;
using FluentValidation;


namespace Application.Orders.CreateOrder
{
    public sealed class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
    {
        public OrderItemDtoValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("ProductName is required.");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero.");
            RuleFor(x => x.UnitPrice).GreaterThan(0).WithMessage("UnitPrice must be greater than zero.");
        }
    }
}
