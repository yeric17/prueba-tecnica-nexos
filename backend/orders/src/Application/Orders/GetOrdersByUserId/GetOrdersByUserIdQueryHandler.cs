using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Orders.DTOs;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Orders.GetOrdersByUserId
{
    public sealed class GetOrdersByUserIdQueryHandler : IQueryHandler<GetOrdersByUserIdQuery, List<OrderDto>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetOrdersByUserIdQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<List<OrderDto>>> Handle(GetOrdersByUserIdQuery query, CancellationToken cancellationToken)
        {
            var orders = await _dbContext.Orders
                .Include(order => order.Items)
                .Where(order => order.UserId == query.UserId)
                .ToListAsync(cancellationToken);

            var ordersDto = orders
                .Select(OrderDto.FromOrder)
                .ToList();

            return ordersDto;
        }
    }
}
