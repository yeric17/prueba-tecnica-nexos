using System;

namespace Domain.Payments.DTOs
{
    public record CreatePaymentDto
    {
        public Guid UserId { get; init; }
        public Guid OrderId { get; init; }
        public decimal Amount { get; init; }
        public string Currency { get; init; } = null!;
        public string PaymentMethod { get; init; } = null!;
    }
}
