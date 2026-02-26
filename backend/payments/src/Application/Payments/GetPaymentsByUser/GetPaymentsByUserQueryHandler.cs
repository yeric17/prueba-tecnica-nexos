using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Payments.DTOs;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Payments.GetPaymentsByUser
{
    public sealed class GetPaymentsByUserQueryHandler : IQueryHandler<GetPaymentsByUserQuery, IReadOnlyList<PaymentDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetPaymentsByUserQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<IReadOnlyList<PaymentDto>>> Handle(GetPaymentsByUserQuery query, CancellationToken cancellationToken)
        {
            IReadOnlyList<PaymentDto> payments = await _context.Payments
                .AsNoTracking()
                .Where(payment => payment.UserId == query.UserId)
                .OrderByDescending(payment => payment.CreatedAt)
                .Select(payment => new PaymentDto
                {
                    Id = payment.Id,
                    UserId = payment.UserId,
                    OrderId = payment.OrderId,
                    Amount = payment.Amount,
                    Currency = payment.Currency,
                    Status = payment.Status,
                    PaymentMethod = payment.PaymentMethod,
                    TransactionId = payment.TransactionId,
                    CreatedAt = payment.CreatedAt,
                    ProcessedAt = payment.ProcessedAt
                })
                .ToListAsync(cancellationToken);

            return Result.Success(payments);
        }
    }
}
