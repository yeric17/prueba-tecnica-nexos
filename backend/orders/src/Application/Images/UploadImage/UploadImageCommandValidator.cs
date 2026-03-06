using FluentValidation;

namespace Application.Images.UploadImage
{
    internal sealed class UploadImageCommandValidator : AbstractValidator<UploadImageCommand>
    {
        public UploadImageCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("ProductId must be greater than 0.");

            RuleFor(x => x.File)
                .NotNull()
                .WithMessage("File is required.");
        }
    }
}
