using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Products.DTOs;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using SharedKernel.Errors;

namespace Application.Products.GetProductById
{
    public sealed class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetProductByIdQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<ProductDto>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == query.ProductId, cancellationToken);

            if (product is null)
            {
                return ProductErrors.NotFound(query.ProductId);
            }

            return ProductDto.FromProduct(product);
        }
    }
}
