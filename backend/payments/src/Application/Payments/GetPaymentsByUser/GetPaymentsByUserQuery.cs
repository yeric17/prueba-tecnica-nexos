using Application.Abstractions.Messaging;
using Domain.Payments.DTOs;

namespace Application.Payments.GetPaymentsByUser
{
    public sealed record GetPaymentsByUserQuery(Guid UserId) : IQuery<IReadOnlyList<PaymentDto>>;
}
