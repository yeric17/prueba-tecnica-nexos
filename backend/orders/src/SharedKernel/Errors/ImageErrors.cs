namespace SharedKernel.Errors
{
    public static class ImageErrors
    {
        public static Error NotFound(Guid imageId) => Error.NotFound(
            code: "ImageNotFound",
            description: $"No image found with ID '{imageId}'.");

        public static Error InvalidFileType() => Error.Failure(
            code: "InvalidFileType",
            description: "Only image files (jpg, jpeg, png, gif, webp) are allowed.");

        public static Error FileTooLarge(long maxSizeInMB) => Error.Failure(
            code: "FileTooLarge",
            description: $"File size exceeds the maximum allowed size of {maxSizeInMB}MB.");

        public static Error UploadFailed(string reason) => Error.Failure(
            code: "ImageUploadFailed",
            description: $"Failed to upload image: {reason}");
    }
}
