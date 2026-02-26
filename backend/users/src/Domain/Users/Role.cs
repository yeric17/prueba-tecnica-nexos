using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Users
{
    public class Role : IdentityRole<Guid>
    {
        public DateTimeOffset CreatedAt { get; set; }

        private Role()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTimeOffset.UtcNow;
            ConcurrencyStamp = Guid.NewGuid().ToString();
        }

        public Role(string name) : this()
        {
            Name = name;
            NormalizedName = name.ToUpperInvariant();
            ConcurrencyStamp = Guid.NewGuid().ToString();
        }
    }
    public class UserRole : IdentityUserRole<Guid> { }
    public class UserClaim : IdentityUserClaim<Guid> { }
    public class UserLogin : IdentityUserLogin<Guid> { }
    public class RoleClaim : IdentityRoleClaim<Guid> { }
    public class UserToken : IdentityUserToken<Guid> { }
}
