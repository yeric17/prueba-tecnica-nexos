using Application.Abstractions.Messaging;

namespace Application.Products.DeleteProduct
{
    public record DeleteProductCommand : ICommand
    {
        public int ProductId { get; set; }
    }
}
