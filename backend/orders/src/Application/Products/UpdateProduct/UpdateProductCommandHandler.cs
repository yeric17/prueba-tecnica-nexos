using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Products.DTOs;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using SharedKernel.Errors;

namespace Application.Products.UpdateProduct
{
    public sealed class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateProductCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products
                .FirstOrDefaultAsync(p => p.Id == command.ProductId, cancellationToken);

            if (product is null)
            {
                return ProductErrors.NotFound(command.ProductId);
            }

            var dto = new UpdateProductDto
            {
                Name = command.Name,
                Description = command.Description,
                Price = command.Price,
                Category = command.Category,
                StockQuantity = command.StockQuantity,
                ImageUrl = command.ImageUrl,
                IsActive = command.IsActive
            };

            product.Update(dto);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
