using System;

namespace Domain.Payments.DTOs
{
    public class PaymentDto
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init; }
        public Guid OrderId { get; init; }
        public decimal Amount { get; init; }
        public string Currency { get; init; } = null!;
        public string Status { get; init; } = null!;
        public string PaymentMethod { get; init; } = null!;
        public string? TransactionId { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime? ProcessedAt { get; init; }

        public static Payment FromPaymentDto(PaymentDto dto)
        {
            return Payment.FromPaymentDto(dto);
        }
    }
}
