using Application.Abstractions.Messaging;
using Domain.Images.DTOs;

namespace Application.Images.GetImagesByProductId
{
    public record GetImagesByProductIdQuery : IQuery<List<ImageDto>>
    {
        public int ProductId { get; set; }
    }
}
