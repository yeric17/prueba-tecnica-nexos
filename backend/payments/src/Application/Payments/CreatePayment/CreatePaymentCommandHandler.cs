

using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Payments;
using Domain.Payments.DTOs;
using SharedKernel;

namespace Application.Payments.CreatePayment
{
    public sealed class CreatePaymentCommandHandler : ICommandHandler<CreatePaymentCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreatePaymentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(CreatePaymentCommand command, CancellationToken cancellationToken)
        {
            CreatePaymentDto createPaymentDto = new CreatePaymentDto
            {
                UserId = command.UserId,
                OrderId = command.OrderId,
                Amount = command.Amount,
                Currency = command.Currency,
                PaymentMethod = command.PaymentMethod
            };

            Payment payment = new(createPaymentDto);

            _context.Payments.Add(payment);

            await _context.SaveChangesAsync();

            return Result.Success();
        }
    }   
}
