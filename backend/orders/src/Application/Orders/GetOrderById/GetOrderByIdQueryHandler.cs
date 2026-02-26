using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Orders.DTOs;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using SharedKernel.Errors;

namespace Application.Orders.GetOrderById
{
    public sealed class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetOrderByIdQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<OrderDto>> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == query.OrderId, cancellationToken);

            if (order is null)
            {
                return OrderErrors.NotFound(query.OrderId);
            }

            return OrderDto.FromOrder(order);
        }
    }
}
