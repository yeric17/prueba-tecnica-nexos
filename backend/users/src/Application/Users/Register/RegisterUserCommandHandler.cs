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
    private string _defaultRole = "User";

    private readonly EmailAddressAttribute _emailAddressAttribute;
    public RegisterUserCommandHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _emailAddressAttribute = new EmailAddressAttribute();
    }

    public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var userManager = _serviceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = _serviceProvider.GetRequiredService<RoleManager<Role>>();

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

        var roleExists = await roleManager.RoleExistsAsync(_defaultRole);
        if (!roleExists)
        {
            return UserErrors.RoleNotFound;
        }


        var user = new User();
        await userStore.SetUserNameAsync(user, request.UserName, cancellationToken);
        await emailStore.SetEmailAsync(user, email, cancellationToken);
        var result = await userManager.CreateAsync(user, request.Password);

        await userManager.AddToRoleAsync(user, _defaultRole);

        if (!result.Succeeded)
        {
            return UserErrors.InvalidIdentity;
        }

        return Result.Success();
    }

    

}