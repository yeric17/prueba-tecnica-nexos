using System;
using System.Collections.Generic;
using System.Text;
using Domain.Payments.DTOs;

namespace Domain.Payments
{
    public class Payment
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

 
        public Guid UserId { get; private set; }
        public Guid OrderId { get; private set; }

 
        public decimal Amount { get; private set; }
        public string Currency { get; private set; } = "USD";
        public string Status { get; private set; } = "Pending"; // Pending, Completed, Failed, Refunded
        public string PaymentMethod { get; private set; } = "CreditCard"; // CreditCard, DebitCard, PayPal
        public string? TransactionId { get; private set; } // ID externo del proveedor de pagos

      
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? ProcessedAt { get; private set; }

        private Payment() { }

        public Payment(CreatePaymentDto dto)
        {
            UserId = dto.UserId;
            OrderId = dto.OrderId;
            Amount = dto.Amount;
            Currency = dto.Currency;
            PaymentMethod = dto.PaymentMethod;
            Status = "Pending";
            CreatedAt = DateTime.UtcNow;
        }


        public static Payment FromPaymentDto(PaymentDto dto)
        {
            return new Payment
            {
                Id = dto.Id,
                UserId = dto.UserId,
                OrderId = dto.OrderId,
                Amount = dto.Amount,
                Currency = dto.Currency,
                Status = dto.Status,
                PaymentMethod = dto.PaymentMethod,
                TransactionId = dto.TransactionId,
                CreatedAt = dto.CreatedAt,
                ProcessedAt = dto.ProcessedAt
            };
        }

        public void MarkAsCompleted(string transactionId)
        {
            Status = "Completed";
            TransactionId = transactionId;
            ProcessedAt = DateTime.UtcNow;
        }


        public void MarkAsFailed()
        {
            Status = "Failed";
            ProcessedAt = DateTime.UtcNow;
        }
    }
}
