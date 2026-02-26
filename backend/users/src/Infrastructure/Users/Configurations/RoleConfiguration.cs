using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.Users.Configurations
{
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("roles");


            SeedData(builder);
        }

        private void SeedData(EntityTypeBuilder<Role> builder)
        {
            Role userRole = new Role("User")
            {
                Id = Guid.Parse("81323341-c0ba-4a2e-ba04-e0bc676ecd95"),
                ConcurrencyStamp = "6da5f32b-1650-4d07-9eed-a4bd06af51e7",
                CreatedAt = new DateTimeOffset(2026, 2, 26, 15, 12, 43, 366, TimeSpan.Zero).AddTicks(7063)
            };

            Role adminRole = new Role("Admin")
            {
                Id = Guid.Parse("19ecc225-42c2-4fbd-9a1e-dc1685472957"),
                ConcurrencyStamp = "a9a67a90-2b69-4976-8d96-432a74cc86ff",
                CreatedAt = new DateTimeOffset(2026, 2, 26, 15, 12, 43, 366, TimeSpan.Zero).AddTicks(8216)
            };

            builder.HasData(userRole, adminRole);
        }
    } 
}
