using Application.Abstractions.Authentication;
using Application.Abstractions.Messaging;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel;
using SharedKernel.Errors;

namespace Application.Users
{
    public class LoginCommandHandler : ICommandHandler<LoginCommand, LoginCommandResponse>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ITokenProvider _tokenProvider;

        public LoginCommandHandler(IServiceProvider sp, ITokenProvider tokenProvider)
        {
            this._serviceProvider = sp;
            _tokenProvider = tokenProvider;
        }

        public async Task<Result<LoginCommandResponse>> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var signInManager = _serviceProvider.GetRequiredService<SignInManager<User>>();

            var user = await signInManager.UserManager.FindByEmailAsync(command.Email);

            if (user is null)
            {
                return UserErrors.Unauthorized;
            }

            var result = await signInManager.PasswordSignInAsync(user.UserName, command.Password, false, lockoutOnFailure: false);


            if (result.Succeeded)
            {

                var token = _tokenProvider.Create(user);
                return new LoginCommandResponse
                {
                    AccessToken = token
                };
            }

            return UserErrors.Unauthorized;
        }
    }
}
