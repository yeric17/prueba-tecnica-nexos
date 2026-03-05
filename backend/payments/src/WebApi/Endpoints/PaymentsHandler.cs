using Application.Abstractions.Messaging;
using Application.Payments.CreatePayment;
using Application.Payments.GetPaymentsByUser;
using Domain.Payments.DTOs;
using SharedKernel;
using WebApi.Extensions;
using WebApi.Infrastructure;

namespace WebApi.Endpoints
{
    public static class PaymentsHandler
    {
        public static RouteGroupBuilder MapPaymentsEndpoints(this RouteGroupBuilder builder)
        {
            var group = builder.MapGroup("payments").WithTags("Payments").RequireAuthorization();

            group.MapPost("/", CreatePayment)
                .WithName("CreatePayment");

            group.MapGet("/user/{userId:guid}", GetPaymentsByUser)
                .WithName("GetPaymentsByUser");

            return builder;
        }

        public static async Task<IResult> CreatePayment(
            ICommandHandler<CreatePaymentCommand> handler,
            CreatePaymentCommand command,
            CancellationToken cancellationToken
        )
        {
            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        }

        public static async Task<IResult> GetPaymentsByUser(
            IQueryHandler<GetPaymentsByUserQuery, IReadOnlyList<PaymentDto>> handler,
            Guid userId,
            CancellationToken cancellationToken
        )
        {
            var query = new GetPaymentsByUserQuery(userId);
            Result<IReadOnlyList<PaymentDto>> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        }
    }
}
