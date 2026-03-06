using Domain.Images;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Images.Configurations
{
    internal class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.FileName).IsRequired().HasMaxLength(255);
            builder.Property(e => e.FilePath).IsRequired().HasMaxLength(500);
            builder.Property(e => e.ContentType).IsRequired().HasMaxLength(100);
            builder.Property(e => e.FileSize).IsRequired();
            builder.Property(e => e.IsPrimary).IsRequired();
            builder.Property(e => e.CreatedAt).IsRequired();

            builder.HasIndex(e => e.ProductId);
            builder.HasIndex(e => new { e.ProductId, e.IsPrimary });

            builder.HasOne(e => e.Product)
                   .WithMany(p => p.Images)
                   .HasForeignKey(e => e.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
