using Application.Abstractions.Messaging;

namespace Application.Users.Register;

public record RegisterUserCommand : ICommand
{
    public string UserName { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string Email { get; init; } = null!;

}
