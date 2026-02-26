
using Application.Abstractions.Data;

using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Database
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }



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
