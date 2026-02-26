using Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Orders.Configurations
{
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.ProductName).IsRequired();
            builder.Property(e => e.Quantity).IsRequired();
            builder.Property(e => e.UnitPrice).HasPrecision(18, 2);
            builder.Property(e => e.Subtotal).HasPrecision(18, 2);
            builder.HasIndex(e => e.OrderId);

            builder.HasOne(e => e.Order)
                   .WithMany(o => o.Items)
                   .HasForeignKey(e => e.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
