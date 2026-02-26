using Application.Abstractions.Messaging;
using Application.Orders.CreateOrder;
using Application.Orders.DeleteOrder;
using Application.Orders.GetOrderById;
using Application.Orders.GetOrdersByUserId;
using Application.Orders.GetUserProducts;
using Application.Orders.UpdateOrder;
using Domain.Orders.DTOs;
using SharedKernel;
using WebApi.Extensions;
using WebApi.Infrastructure;

namespace WebApi.Endpoints
{
    public static class OrdersHandler
    {
        public static RouteGroupBuilder MapOrdersEndpoints(this RouteGroupBuilder builder)
        {
            var orders = builder.MapGroup("/orders");

            orders.MapPost("", CreateOrder)
                .WithName("CreateOrder")
                .Produces(StatusCodes.Status200OK);

            orders.MapPut("/{orderId:guid}", UpdateOrder)
                .WithName("UpdateOrder")
                .Produces(StatusCodes.Status204NoContent);

            orders.MapDelete("/{orderId:guid}", DeleteOrder)
                .WithName("DeleteOrder")
                .Produces(StatusCodes.Status204NoContent);

            orders.MapGet("/{orderId:guid}", GetOrderById)
                .WithName("GetOrderById")
                .Produces<OrderDto>(StatusCodes.Status200OK);

            orders.MapGet("/user/{userId}", GetOrdersByUserId)
                .WithName("GetOrdersByUserId")
                .Produces<List<OrderDto>>(StatusCodes.Status200OK);

            orders.MapGet("/user/{userId}/products", GetUserProducts)
                .WithName("GetUserProducts")
                .Produces<List<OrderItemDto>>(StatusCodes.Status200OK);

            return builder;
        }

        public static async Task<IResult> CreateOrder(
            ICommandHandler<CreateOrderCommand, Guid> handler,
            CreateOrderCommand command,
            CancellationToken cancellationToken
            )
        {
            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(() => Results.Ok(new { Id = result.Value}), CustomResults.Problem);
        }

        public static async Task<IResult> UpdateOrder(
            ICommandHandler<UpdateOrderCommand> handler,
            Guid orderId,
            UpdateOrderCommand command,
            CancellationToken cancellationToken)
        {
            command.OrderId = orderId;

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        }

        public static async Task<IResult> DeleteOrder(
            ICommandHandler<DeleteOrderCommand> handler,
            Guid orderId,
            CancellationToken cancellationToken)
        {
            Result result = await handler.Handle(new DeleteOrderCommand { OrderId = orderId }, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        }

        public static async Task<IResult> GetOrderById(
            IQueryHandler<GetOrderByIdQuery, OrderDto> handler,
            Guid orderId,
            CancellationToken cancellationToken)
        {
            Result<OrderDto> result = await handler.Handle(new GetOrderByIdQuery { OrderId = orderId }, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        }

        public static async Task<IResult> GetOrdersByUserId(
            IQueryHandler<GetOrdersByUserIdQuery, List<OrderDto>> handler,
            Guid userId,
            CancellationToken cancellationToken)
        {
            Result<List<OrderDto>> result = await handler.Handle(new GetOrdersByUserIdQuery { UserId = userId }, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        }

        public static async Task<IResult> GetUserProducts(
            IQueryHandler<GetUserProductsQuery, List<OrderItemDto>> handler,
            Guid userId,
            CancellationToken cancellationToken)
        {
            Result<List<OrderItemDto>> result = await handler.Handle(new GetUserProductsQuery { UserId = userId }, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        }
    }
}
