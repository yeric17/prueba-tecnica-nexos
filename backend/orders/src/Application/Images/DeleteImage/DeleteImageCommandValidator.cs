using FluentValidation;

namespace Application.Images.DeleteImage
{
    internal sealed class DeleteImageCommandValidator : AbstractValidator<DeleteImageCommand>
    {
        public DeleteImageCommandValidator()
        {
            RuleFor(x => x.ImageId)
                .NotEmpty()
                .WithMessage("ImageId is required.");
        }
    }
}
