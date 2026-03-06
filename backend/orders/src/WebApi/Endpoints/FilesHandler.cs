using Application.Abstractions.Storage;
using WebApi.Infrastructure;

namespace WebApi.Endpoints
{
    public static class FilesHandler
    {
        public static RouteGroupBuilder MapFilesEndpoints(this RouteGroupBuilder builder)
        {
            var files = builder.MapGroup("/files");

            files.MapGet("/images/{*filePath}", GetImage)
                .WithName("GetImage")
                .AllowAnonymous();

            return builder;
        }

        public static async Task<IResult> GetImage(
            IFileStorageService fileStorageService,
            string filePath,
            CancellationToken cancellationToken)
        {
            var fileResult = await fileStorageService.GetFileAsync(filePath, cancellationToken);

            if (fileResult == null)
            {
                return Results.NotFound();
            }

            var (fileStream, contentType) = fileResult.Value;

            return Results.File(fileStream, contentType, enableRangeProcessing: true);
        }
    }
}
