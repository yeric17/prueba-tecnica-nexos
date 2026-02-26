using Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Users
{
    public record LoginCommand :ICommand<LoginCommandResponse>
    {
        public string Email { get; init; } = null!;
        public string Password { get; init; } = null!;
    }
}
