using Application.Abstractions.Messaging;
using Domain.Users.DTOs;

namespace Application.Users;
public record GetMeQuery : IQuery<UserDto>
{
        
}
