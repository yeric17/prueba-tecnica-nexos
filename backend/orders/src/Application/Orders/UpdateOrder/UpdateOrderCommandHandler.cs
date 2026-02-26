using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Orders.DTOs;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using SharedKernel.Errors;

namespace Application.Orders.UpdateOrder
{
    public sealed class UpdateOrderCommandHandler : ICommandHandler<UpdateOrderCommand>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateOrderCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders
                .Include(order => order.Items)
                .FirstOrDefaultAsync(order => order.Id == command.OrderId, cancellationToken);

            if (order is null)
            {
                return Error.NotFound(
                    code: "Order.NotFound",
                    description: $"Order with id '{command.OrderId}' was not found.");
            }

            var dto = new UpdateOrderDto
            {
                Items = command.Items,
                Status = command.Status,
                ShippingAddress = command.ShippingAddress,
                ShippingCity = command.ShippingCity,
                ShippingCountry = command.ShippingCountry
            };

            order.Update(dto);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}