using FluentValidation;

namespace Application.Orders.GetUserProducts
{
    public sealed class GetUserProductsQueryValidator : AbstractValidator<GetUserProductsQuery>
    {
        public GetUserProductsQueryValidator()
        {
            RuleFor(query => query.UserId).NotEmpty().WithMessage("UserId is required.");
        }
    }
}