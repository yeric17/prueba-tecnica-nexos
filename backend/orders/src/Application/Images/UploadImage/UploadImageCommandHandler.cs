using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Abstractions.Storage;
using Domain.Images;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using SharedKernel.Errors;

namespace Application.Images.UploadImage
{
    internal sealed class UploadImageCommandHandler : ICommandHandler<UploadImageCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileStorageService _fileStorageService;
        private const long MaxFileSizeInBytes = 5 * 1024 * 1024; // 5MB
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

        public UploadImageCommandHandler(IApplicationDbContext context, IFileStorageService fileStorageService)
        {
            _context = context;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<Guid>> Handle(UploadImageCommand command, CancellationToken cancellationToken)
        {
            // Validate product exists
            var productExists = await _context.Products
                .AnyAsync(p => p.Id == command.ProductId, cancellationToken);

            if (!productExists)
            {
                return Result.Failure<Guid>(ProductErrors.NotFound(command.ProductId));
            }

            // Validate file
            if (command.File == null || command.File.Length == 0)
            {
                return Result.Failure<Guid>(ImageErrors.InvalidFileType());
            }

            var fileExtension = Path.GetExtension(command.File.FileName).ToLowerInvariant();
            if (!AllowedExtensions.Contains(fileExtension))
            {
                return Result.Failure<Guid>(ImageErrors.InvalidFileType());
            }

            if (command.File.Length > MaxFileSizeInBytes)
            {
                return Result.Failure<Guid>(ImageErrors.FileTooLarge(5));
            }

            // If this image is set as primary, unset other primary images
            if (command.IsPrimary)
            {
                var existingPrimaryImages = await _context.Images
                    .Where(i => i.ProductId == command.ProductId && i.IsPrimary)
                    .ToListAsync(cancellationToken);

                foreach (var img in existingPrimaryImages)
                {
                    img.UnsetPrimary();
                }
            }

            // Save file to storage
            string filePath;
            try
            {
                using var stream = command.File.OpenReadStream();
                filePath = await _fileStorageService.SaveFileAsync(
                    stream,
                    command.File.FileName,
                    command.File.ContentType,
                    cancellationToken);
            }
            catch (Exception ex)
            {
                return Result.Failure<Guid>(ImageErrors.UploadFailed(ex.Message));
            }

            // Create image entity
            var image = new Image(
                command.ProductId,
                command.File.FileName,
                filePath,
                command.File.ContentType,
                command.File.Length,
                command.IsPrimary);

            _context.Images.Add(image);
            await _context.SaveChangesAsync(cancellationToken);

            return image.Id;
        }
    }
}
