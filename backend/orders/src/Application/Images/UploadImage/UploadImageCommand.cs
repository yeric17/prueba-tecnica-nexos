using Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;

namespace Application.Images.UploadImage
{
    public record UploadImageCommand : ICommand<Guid>
    {
        public int ProductId { get; set; }
        public IFormFile File { get; set; } = null!;
        public bool IsPrimary { get; set; }
    }
}
