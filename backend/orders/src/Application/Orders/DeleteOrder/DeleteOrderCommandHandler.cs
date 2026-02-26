using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using SharedKernel.Errors;

namespace Application.Orders.DeleteOrder
{
    public sealed class DeleteOrderCommandHandler : ICommandHandler<DeleteOrderCommand>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteOrderCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders
                .FirstOrDefaultAsync(order => order.Id == command.OrderId, cancellationToken);

            if (order is null)
            {
                return OrderErrors.NotFound(command.OrderId);
            }

            _dbContext.Orders.Remove(order);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}