namespace Domain.Images.DTOs
{
    public record ImageDto
    {
        public Guid Id { get; init; }
        public int ProductId { get; init; }
        public string FileName { get; init; } = string.Empty;
        public string FilePath { get; init; } = string.Empty;
        public string ContentType { get; init; } = string.Empty;
        public long FileSize { get; init; }
        public bool IsPrimary { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
    }
}
