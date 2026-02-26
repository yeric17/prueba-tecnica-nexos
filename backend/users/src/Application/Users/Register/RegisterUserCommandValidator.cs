using FluentValidation;


namespace Application.Users;

public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .MinimumLength(3);
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}
