using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Users.DTOs
{
    public record UserDto
    {
        public Guid Id { get; init; }
        public string? UserName { get; init; }
        
        public string? Email { get; init; }

        public string[] Roles { get; private set; } = [];

        public static UserDto FromUser(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };
        }

        public UserDto WithRoles(string[] roles)
        {
            Roles = roles;
            return this;
        }
    }
}
