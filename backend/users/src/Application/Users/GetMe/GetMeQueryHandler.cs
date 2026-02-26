using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Users;
using Domain.Users.DTOs;
using SharedKernel;
using SharedKernel.Errors;

namespace Application.Users;

public class GetMeQueryHandler : IQueryHandler<GetMeQuery, UserDto>
{
    private readonly IUserContext _userContext;
    private readonly IApplicationDbContext _dbContext;

    public GetMeQueryHandler(IUserContext userContext, IApplicationDbContext dbContext)
    {
        _userContext = userContext;
        _dbContext = dbContext;
    }

    public async Task<Result<UserDto>> Handle(GetMeQuery query, CancellationToken cancellationToken)
    {
        Guid userId = _userContext.UserId;

        User? user = await _dbContext.Users.FindAsync(userId);

        if (user is null) {
            return UserErrors.UserNotFound(userId);
        }

        return UserDto.FromUser(user);
    }
}
