using Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Authentication
{
    internal class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId =>
            _httpContextAccessor
                .HttpContext?
                .User
                .GetUserId() ??
            throw new UserContextUnavailableException();
    }
}
