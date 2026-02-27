using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Orders.DTOs;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Orders.GetAllOrders
{
    public sealed class GetAllOrdersQueryHandler : IQueryHandler<GetAllOrdersQuery, List<OrderDto>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetAllOrdersQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<List<OrderDto>>> Handle(GetAllOrdersQuery query, CancellationToken cancellationToken)
        {
            // Obtener todas las órdenes ordenadas por fecha de creación (más recientes primero)
            var orders = await _dbContext.Orders
                .Include(o => o.Items)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync(cancellationToken);

            var ordersDto = orders
                .Select(OrderDto.FromOrder)
                .ToList();

            return ordersDto;
        }
    }
}
