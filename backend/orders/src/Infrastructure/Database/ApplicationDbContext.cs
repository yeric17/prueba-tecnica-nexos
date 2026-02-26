
using Application.Abstractions.Data;
using Domain.Orders;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Database
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; }
        public DbSet<OrderItem> OrderItems { get; }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
            int result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
