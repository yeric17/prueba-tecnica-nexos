using Application.Abstractions.Messaging;
using Domain.Products.DTOs;

namespace Application.Products.GetProductById
{
    public record GetProductByIdQuery : IQuery<ProductDto>
    {
        public int ProductId { get; set; }
    }
}
