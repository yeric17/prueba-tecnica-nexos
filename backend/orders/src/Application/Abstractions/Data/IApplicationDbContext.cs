using Domain.Images;
using Domain.Orders;
using Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Order> Orders { get; set; }
        DbSet<OrderItem> OrderItems { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Image> Images { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
