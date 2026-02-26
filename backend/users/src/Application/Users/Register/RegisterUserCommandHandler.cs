using Application.Abstractions.Messaging;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel;
using SharedKernel.Errors;
using System.ComponentModel.DataAnnotations;

namespace Application.Users;

public sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly IServiceProvider _serviceProvider;

    private readonly EmailAddressAttribute _emailAddressAttribute;
    public RegisterUserCommandHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _emailAddressAttribute = new EmailAddressAttribute();
    }

    public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var userManager = _serviceProvider.GetRequiredService<UserManager<User>>();

        if (!userManager.SupportsUserEmail)
        {
            return UserErrors.RequiredEmail;
        }

        var userStore = _serviceProvider.GetRequiredService<IUserStore<User>>();
        var emailStore = (IUserEmailStore<User>)userStore;
        var email = request.Email;


        if (string.IsNullOrEmpty(email) || !_emailAddressAttribute.IsValid(email))
        {
            return UserErrors.InvalidEmail;
        }


        var user = new User();
        await userStore.SetUserNameAsync(user, email, CancellationToken.None);
        await emailStore.SetEmailAsync(user, email, CancellationToken.None);
        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return UserErrors.InvalidIdentity;
        }

        return Result.Success();
    }

    

}