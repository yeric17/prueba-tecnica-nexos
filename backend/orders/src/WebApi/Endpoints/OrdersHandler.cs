using Application.Abstractions.Messaging;
using Application.Orders.CreateOrder;
using SharedKernel;
using WebApi.Extensions;
using WebApi.Infrastructure;

namespace Orders.WebApi.Endpoints
{
    public static class OrdersHandler
    {
        public static RouteGroupBuilder MapOrdersEndpoints(this RouteGroupBuilder builder)
        {
    
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
    }
}
