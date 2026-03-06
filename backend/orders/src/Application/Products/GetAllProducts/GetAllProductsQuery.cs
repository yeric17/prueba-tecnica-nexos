using Application.Abstractions.Messaging;
using Domain.Products.DTOs;

namespace Application.Products.GetAllProducts
{
    public record GetAllProductsQuery : IQuery<List<ProductDto>>
    {
    }
}
