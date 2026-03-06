using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Products;
using Domain.Products.DTOs;
using SharedKernel;

namespace Application.Products.CreateProduct
{
    public sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, int>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateProductCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<int>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            CreateProductDto dto = new CreateProductDto
            {
                Name = command.Name,
                Description = command.Description,
                Price = command.Price,
                Category = command.Category,
                StockQuantity = command.StockQuantity,
                ImageUrl = command.ImageUrl
            };

            Product product = new(dto);

            _dbContext.Products.Add(product);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}
