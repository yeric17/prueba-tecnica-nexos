using Application.Abstractions.Messaging;

namespace Application.Payments.CreatePayment
{
    public record CreatePaymentCommand : ICommand
    {
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "COP";
        public string PaymentMethod { get; set; } = "CreditCard";
    }
}
