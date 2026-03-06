using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using SharedKernel.Errors;

namespace Application.Products.DeleteProduct
{
    public sealed class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteProductCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products
                .FirstOrDefaultAsync(p => p.Id == command.ProductId, cancellationToken);

            if (product is null)
            {
                return ProductErrors.NotFound(command.ProductId);
            }

            _dbContext.Products.Remove(product);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
