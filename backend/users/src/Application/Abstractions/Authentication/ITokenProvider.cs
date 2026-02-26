using Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Authentication
{
    public interface ITokenProvider
    {
        string Create(User user);
    }
}
