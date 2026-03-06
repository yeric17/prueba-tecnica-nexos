using Application.Abstractions.Storage;

namespace Infrastructure.Storage
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly string _baseDirectory;
        private readonly string _baseUrl;

        public LocalFileStorageService()
        {
            _baseDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Files", "Images");
            _baseUrl = "/files/images";

            // Ensure directory exists
            if (!Directory.Exists(_baseDirectory))
            {
                Directory.CreateDirectory(_baseDirectory);
            }
        }

        public async Task<string> SaveFileAsync(Stream fileStream, string fileName, string contentType, CancellationToken cancellationToken = default)
        {
            try
            {
                // Generate unique filename
                var fileExtension = Path.GetExtension(fileName);
                var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
                var filePath = Path.Combine(_baseDirectory, uniqueFileName);

                // Save file
                using (var outputStream = new FileStream(filePath, FileMode.Create))
                {
                    await fileStream.CopyToAsync(outputStream, cancellationToken);
                }

                // Return relative path
                return Path.Combine("Images", uniqueFileName).Replace("\\", "/");
            }
            catch (Exception ex)
            {
                throw new IOException($"Error saving file: {ex.Message}", ex);
            }
        }

        public Task<bool> DeleteFileAsync(string filePath, CancellationToken cancellationToken = default)
        {
            try
            {
                var fullPath = BuildFullPath(filePath);
                
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return Task.FromResult(true);
                }

                return Task.FromResult(false);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }

        public Task<(Stream FileStream, string ContentType)?> GetFileAsync(string filePath, CancellationToken cancellationToken = default)
        {
            try
            {
                var fullPath = BuildFullPath(filePath);

                if (!File.Exists(fullPath))
                {
                    return Task.FromResult<(Stream, string)?>(null);
                }

                var fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
                var contentType = GetContentType(Path.GetExtension(fullPath));

                return Task.FromResult<(Stream, string)?>((fileStream, contentType));
            }
            catch (Exception)
            {
                return Task.FromResult<(Stream, string)?>(null);
            }
        }

        public Task<bool> FileExistsAsync(string filePath, CancellationToken cancellationToken = default)
        {
            var fullPath = BuildFullPath(filePath);
            return Task.FromResult(File.Exists(fullPath));
        }

        private string BuildFullPath(string relativeFilePath)
        {
            // Split by either slash type to handle paths stored with any separator
            var segments = relativeFilePath.Split('/', '\\', StringSplitOptions.RemoveEmptyEntries);
            var parts = new[] { Directory.GetCurrentDirectory(), "Files" }.Concat(segments).ToArray();
            return Path.Combine(parts);
        }

        public string GetFileUrl(string filePath)
        {
            return $"{_baseUrl}/{filePath}";
        }

        private string GetContentType(string extension)
        {
            return extension.ToLowerInvariant() switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".webp" => "image/webp",
                ".bmp" => "image/bmp",
                _ => "application/octet-stream"
            };
        }
    }
}
