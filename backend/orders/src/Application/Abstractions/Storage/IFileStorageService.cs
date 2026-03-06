namespace Application.Abstractions.Storage
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(Stream fileStream, string fileName, string contentType, CancellationToken cancellationToken = default);
        Task<bool> DeleteFileAsync(string filePath, CancellationToken cancellationToken = default);
        Task<(Stream FileStream, string ContentType)?> GetFileAsync(string filePath, CancellationToken cancellationToken = default);
        Task<bool> FileExistsAsync(string filePath, CancellationToken cancellationToken = default);
        string GetFileUrl(string filePath);
    }
}
