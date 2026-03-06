using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Products.DTOs;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Products.GetAllProducts
{
    public sealed class GetAllProductsQueryHandler : IQueryHandler<GetAllProductsQuery, List<ProductDto>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetAllProductsQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<List<ProductDto>>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await _dbContext.Products
                .Include(p => p.Images)
                .Where(p => p.IsActive)
                .OrderBy(p => p.Name)
                .ToListAsync(cancellationToken);

            var productsDto = products
                .Select(ProductDto.FromProduct)
                .ToList();

            return productsDto;
        }
    }
}
