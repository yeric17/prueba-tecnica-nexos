using FluentValidation;

namespace Application.Orders.GetOrdersByUserId
{
    public sealed class GetOrdersByUserIdQueryValidator : AbstractValidator<GetOrdersByUserIdQuery>
    {
        public GetOrdersByUserIdQueryValidator()
        {
            RuleFor(query => query.UserId)
                .NotEmpty()
                .WithMessage("UserId is required.");
        }
    }
}
