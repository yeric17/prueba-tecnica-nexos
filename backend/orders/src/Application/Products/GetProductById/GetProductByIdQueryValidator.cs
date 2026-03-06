using FluentValidation;

namespace Application.Products.GetProductById
{
    public sealed class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdQueryValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("ProductId must be greater than zero.");
        }
    }
}
