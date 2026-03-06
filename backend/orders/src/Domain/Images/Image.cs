namespace Domain.Images
{
    public class Image
    {
        public Guid Id { get; private set; }
        public int ProductId { get; private set; }
        public string FileName { get; private set; } = string.Empty;
        public string FilePath { get; private set; } = string.Empty;
        public string ContentType { get; private set; } = string.Empty;
        public long FileSize { get; private set; }
        public bool IsPrimary { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }

        #region Navigation Properties

        public Products.Product Product { get; private set; } = null!;

        #endregion

        private Image() { }

        public Image(int productId, string fileName, string filePath, string contentType, long fileSize, bool isPrimary = false)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            FileName = fileName;
            FilePath = filePath;
            ContentType = contentType;
            FileSize = fileSize;
            IsPrimary = isPrimary;
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public void SetAsPrimary()
        {
            IsPrimary = true;
        }

        public void UnsetPrimary()
        {
            IsPrimary = false;
        }
    }
}
