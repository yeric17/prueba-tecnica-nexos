using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Abstractions.Storage;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using SharedKernel.Errors;

namespace Application.Images.DeleteImage
{
    internal sealed class DeleteImageCommandHandler : ICommandHandler<DeleteImageCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileStorageService _fileStorageService;

        public DeleteImageCommandHandler(IApplicationDbContext context, IFileStorageService fileStorageService)
        {
            _context = context;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result> Handle(DeleteImageCommand command, CancellationToken cancellationToken)
        {
            var image = await _context.Images
                .FirstOrDefaultAsync(i => i.Id == command.ImageId, cancellationToken);

            if (image == null)
            {
                return Result.Failure(ImageErrors.NotFound(command.ImageId));
            }

            // Delete physical file
            await _fileStorageService.DeleteFileAsync(image.FilePath, cancellationToken);

            // Delete from database
            _context.Images.Remove(image);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
