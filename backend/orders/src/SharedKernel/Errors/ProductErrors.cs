namespace SharedKernel.Errors
{
    public static class ProductErrors
    {
        public static Error InternalServerError => Error.Conflict(
            code: "InternalServerError",
            description: "An unexpected error occurred. Please try again later.");

        public static Error NotFound(int productId) => Error.NotFound(
            code: "ProductNotFound",
            description: $"No product found with ID '{productId}'.");
    }
}
