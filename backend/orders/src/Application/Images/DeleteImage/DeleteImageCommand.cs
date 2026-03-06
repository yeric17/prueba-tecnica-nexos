using Application.Abstractions.Messaging;

namespace Application.Images.DeleteImage
{
    public record DeleteImageCommand : ICommand
    {
        public Guid ImageId { get; set; }
    }
}
