using FluentValidation;

namespace Application.Orders.GetOrderById
{
    public sealed class GetOrderByIdQueryValidator : AbstractValidator<GetOrderByIdQuery>
    {
        public GetOrderByIdQueryValidator()
        {
            RuleFor(query => query.OrderId)
                .NotEmpty()
                .WithMessage("OrderId is required.");
        }
    }
}
