using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Orders.DTOs;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Orders.GetUserProducts
{
    public sealed class GetUserProductsQueryHandler : IQueryHandler<GetUserProductsQuery, List<OrderItemDto>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetUserProductsQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<List<OrderItemDto>>> Handle(GetUserProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await _dbContext.Orders
                .Where(order => order.UserId == query.UserId)
                .SelectMany(order => order.Items)
                .Select(item => new OrderItemDto
                {
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                })
                .ToListAsync(cancellationToken) ?? new List<OrderItemDto>();

            return products;
        }
    }
}