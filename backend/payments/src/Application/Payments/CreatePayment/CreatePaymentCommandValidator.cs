using FluentValidation;

namespace Application.Payments.CreatePayment
{
    public sealed class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.OrderId).NotEmpty();
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.Currency)
                .NotEmpty()
                .Length(3)
                .Custom((value, context) =>
                {
                    var allowedCurrencies = new[] { "USD", "EUR", "COP" };
                    if (!allowedCurrencies.Contains(value))
                    {
                        context.AddFailure($"Currency must be one of the following: {string.Join(", ", allowedCurrencies)}");
                    }
                });

            RuleFor(x => x.PaymentMethod)
                .Custom((value, context) =>
                {
                    var allowedMethods = new[] { "CreditCard", "DebitCard", "PayPal" };
                    if (!allowedMethods.Contains(value))
                    {
                        context.AddFailure($"PaymentMethod must be one of the following: {string.Join(", ", allowedMethods)}");
                    }
                })
                .NotEmpty();
        }
    }
}
