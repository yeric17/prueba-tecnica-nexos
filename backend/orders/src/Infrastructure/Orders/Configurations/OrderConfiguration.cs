using Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel;


namespace Infrastructure.Orders.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.OrderNumber).IsRequired().HasMaxLength(50);
            builder.Property(e => e.UserId).IsRequired().HasMaxLength(450);
            builder.Property(e => e.Status).IsRequired().HasMaxLength(20);
            builder.Property(e => e.TotalAmount).HasPrecision(18, 2);

            builder.HasIndex(e => e.UserId);
            builder.HasIndex(e => e.OrderNumber).IsUnique();

            builder.HasMany(e => e.Items)
                   .WithOne()
                   .HasForeignKey(i => i.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
