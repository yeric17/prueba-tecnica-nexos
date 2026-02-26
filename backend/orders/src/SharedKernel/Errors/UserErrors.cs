

using Microsoft.AspNetCore.Identity;

namespace SharedKernel.Errors
{
    public static class OrderErrors
    {


        public static Error InternalServerError => Error.Conflict(
            code: "InternalServerError",
            description: "An unexpected error occurred. Please try again later.");

        public static Error NotFound(Guid orderId) => Error.NotFound(
            code: "OrderNotFound",
            description: $"No order found with ID '{orderId}'.");


    }
}
