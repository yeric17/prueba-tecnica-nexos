using Domain.Products.DTOs;

namespace Domain.Products
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public decimal Price { get; private set; }
        public string? Category { get; private set; }
        public int StockQuantity { get; private set; }
        public string? ImageUrl { get; private set; }
        public bool IsActive { get; private set; } = true;
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }

        #region Navigation Properties
        public List<Images.Image> Images { get; private set; } = new();
        #endregion

        private Product() { }

        public Product(CreateProductDto dto)
        {
            Name = dto.Name;
            Description = dto.Description;
            Price = dto.Price;
            Category = dto.Category;
            StockQuantity = dto.StockQuantity;
            ImageUrl = dto.ImageUrl;
            IsActive = true;
            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void Update(UpdateProductDto dto)
        {
            Name = dto.Name;
            Description = dto.Description;
            Price = dto.Price;
            Category = dto.Category;
            StockQuantity = dto.StockQuantity;
            ImageUrl = dto.ImageUrl;
            IsActive = dto.IsActive;
            UpdatedAt = DateTimeOffset.UtcNow;
        }
    }
}