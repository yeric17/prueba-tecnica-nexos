using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Abstractions.Storage;
using Application.Images.DeleteImage;
using Application.Images.GetImagesByProductId;
using Application.Images.UploadImage;
using Application.Products.CreateProduct;
using Application.Products.DeleteProduct;
using Application.Products.GetAllProducts;
using Application.Products.GetProductById;
using Application.Products.UpdateProduct;
using Domain.Images.DTOs;
using Domain.Products.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using WebApi.Extensions;
using WebApi.Infrastructure;

namespace WebApi.Endpoints
{
    public static class ProductsHandler
    {
        public static RouteGroupBuilder MapProductsEndpoints(this RouteGroupBuilder builder)
        {
            var products = builder.MapGroup("/products");

            products.MapGet("", GetAllProducts)
                .WithName("GetAllProducts")
                .Produces<List<ProductDto>>(StatusCodes.Status200OK)
                .RequireAuthorization();

            products.MapPost("", CreateProduct)
                .WithName("CreateProduct")
                .Produces(StatusCodes.Status200OK)
                .RequireAuthorization();

            products.MapPut("/{productId:int}", UpdateProduct)
                .WithName("UpdateProduct")
                .Produces(StatusCodes.Status204NoContent)
                .RequireAuthorization();

            products.MapDelete("/{productId:int}", DeleteProduct)
                .WithName("DeleteProduct")
                .Produces(StatusCodes.Status204NoContent)
                .RequireAuthorization();

            products.MapGet("/{productId:int}", GetProductById)
                .WithName("GetProductById")
                .Produces<ProductDto>(StatusCodes.Status200OK)
                .RequireAuthorization();

            products.MapGet("/{productId:int}/image", GetProductImage)
                .WithName("GetProductImage")
                .AllowAnonymous()
                .Produces(StatusCodes.Status200OK);

            // Image endpoints
            products.MapPost("/{productId:int}/images", UploadImage)
                .WithName("UploadProductImage")
                .Produces(StatusCodes.Status200OK)
                .RequireAuthorization()
                .DisableAntiforgery();

            products.MapGet("/{productId:int}/images", GetImagesByProductId)
                .WithName("GetProductImages")
                .Produces<List<ImageDto>>(StatusCodes.Status200OK);

            products.MapDelete("/images/{imageId:guid}", DeleteImage)
                .WithName("DeleteProductImage")
                .Produces(StatusCodes.Status204NoContent)
                .RequireAuthorization();

            return builder;
        }

        public static async Task<IResult> CreateProduct(
            ICommandHandler<CreateProductCommand, int> handler,
            CreateProductCommand command,
            CancellationToken cancellationToken)
        {
            Result<int> result = await handler.Handle(command, cancellationToken);

            return result.Match(() => Results.Ok(new { Id = result.Value }), CustomResults.Problem);
        }

        public static async Task<IResult> UpdateProduct(
            ICommandHandler<UpdateProductCommand> handler,
            int productId,
            UpdateProductCommand command,
            CancellationToken cancellationToken)
        {
            command.ProductId = productId;

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        }

        public static async Task<IResult> DeleteProduct(
            ICommandHandler<DeleteProductCommand> handler,
            int productId,
            CancellationToken cancellationToken)
        {
            Result result = await handler.Handle(new DeleteProductCommand { ProductId = productId }, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        }

        public static async Task<IResult> GetProductById(
            IQueryHandler<GetProductByIdQuery, ProductDto> handler,
            int productId,
            CancellationToken cancellationToken)
        {
            Result<ProductDto> result = await handler.Handle(new GetProductByIdQuery { ProductId = productId }, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        }

        public static async Task<IResult> GetProductImage(
            IApplicationDbContext dbContext,
            IFileStorageService fileStorageService,
            int productId,
            CancellationToken cancellationToken)
        {
            var primaryImage = await dbContext.Images
                .Where(i => i.ProductId == productId && i.IsPrimary)
                .FirstOrDefaultAsync(cancellationToken);

            if (primaryImage == null)
            {
                return Results.NotFound();
            }

            var fileResult = await fileStorageService.GetFileAsync(primaryImage.FilePath, cancellationToken);

            if (fileResult == null)
            {
                return Results.NotFound();
            }

            var (fileStream, contentType) = fileResult.Value;

            return Results.File(fileStream, contentType ?? primaryImage.ContentType, enableRangeProcessing: true);
        }

        public static async Task<IResult> GetAllProducts(
            IQueryHandler<GetAllProductsQuery, List<ProductDto>> handler,
            CancellationToken cancellationToken)
        {
            Result<List<ProductDto>> result = await handler.Handle(new GetAllProductsQuery(), cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        }

        // Image handlers
        public static async Task<IResult> UploadImage(
            ICommandHandler<UploadImageCommand, Guid> handler,
            int productId,
            [FromForm] IFormFile file,
            [FromForm] bool isPrimary,
            CancellationToken cancellationToken)
        {
            var command = new UploadImageCommand
            {
                ProductId = productId,
                File = file,
                IsPrimary = isPrimary
            };

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(() => Results.Ok(new { Id = result.Value }), CustomResults.Problem);
        }

        public static async Task<IResult> GetImagesByProductId(
            IQueryHandler<GetImagesByProductIdQuery, List<ImageDto>> handler,
            int productId,
            CancellationToken cancellationToken)
        {
            Result<List<ImageDto>> result = await handler.Handle(new GetImagesByProductIdQuery { ProductId = productId }, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        }

        public static async Task<IResult> DeleteImage(
            ICommandHandler<DeleteImageCommand> handler,
            Guid imageId,
            CancellationToken cancellationToken)
        {
            Result result = await handler.Handle(new DeleteImageCommand { ImageId = imageId }, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        }
    }
}
