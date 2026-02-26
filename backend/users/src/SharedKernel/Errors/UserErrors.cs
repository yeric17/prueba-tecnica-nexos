

using Microsoft.AspNetCore.Identity;

namespace SharedKernel.Errors
{
    public static class UserErrors
    {
        public static Error UserAlreadyExists(string email) => Error.Failure(
                code: "UserAlreadyExists",
                description: $"A user with the email '{email}' already exists.");

        public static Error InvalidIdentity => Error.Failure(
            code: "InvalidIdentity",
            description: "The identity operation failed.");

        public static Error RequiredEmail => Error.Failure(
            code: "RequiredEmail",
            description: "Email is required.");


        public static Error InvalidEmail => Error.Failure(
            code: "InvalidEmail",
            description: "The email address is not valid.");

        public static Error Unauthorized => Error.Failure(
            code: "Unauthorized",
            description: "Unauthorized access.");

        public static Error InternalServerError => Error.Conflict(
            code: "InternalServerError",
            description: "An unexpected error occurred. Please try again later.");

    }
}
