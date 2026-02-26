

using Microsoft.AspNetCore.Identity;

namespace SharedKernel.Errors
{
    public static class PaymentErrors
    {


        public static Error InternalServerError => Error.Conflict(
            code: "InternalServerError",
            description: "An unexpected error occurred. Please try again later.");



    }
}
