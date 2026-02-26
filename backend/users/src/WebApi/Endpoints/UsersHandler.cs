
using Application.Abstractions.Messaging;
using Application.Users;
using Domain.Users.DTOs;
using SharedKernel;
using WebApi.Extensions;
using WebApi.Infrastructure;

namespace WebApi.Endpoints
{
    public static class UsersHandler
    {
        public static RouteGroupBuilder MapUsersEndpoints(this RouteGroupBuilder builder)
        {
            var group = builder.MapGroup("/users").WithTags("Users");

            group.MapPost("/register", RegisterUser)
                .WithName("RegisterUser")
                .Produces(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            return builder;
        }

        public static async Task<IResult> RegisterUser(
            ICommandHandler<RegisterUserCommand> handler,
            RegisterUserCommand request,
            CancellationToken cancellationToken
            )
        {
            Result result = await handler.Handle(request, cancellationToken);

            return result.Match(Results.Created, CustomResults.Problem);
        }

        public static async Task<IResult> GetMe(
            IQueryHandler<GetMeQuery, UserDto> handler,
            CancellationToken cancellationToken
            )
        {
            var query = new GetMeQuery();
            Result<UserDto> result = await handler.Handle(query, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        }
    }
}
