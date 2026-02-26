using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Orders;
using Domain.Orders.DTOs;
using SharedKernel;


namespace Application.Orders.CreateOrder
{
    public sealed class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, Guid>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateOrderCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Guid>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            CreateOrderDto dto = new CreateOrderDto
            {
                UserId = command.UserId,
                Items = command.Items,
                ShippingAddress = command.ShippingAddress,
                ShippingCity = command.ShippingCity,
                ShippingCountry = command.ShippingCountry
            };

            Order order = new(dto);

            _dbContext.Orders.Add(order);

            await _dbContext.SaveChangesAsync();

            return order.Id;
        }
    }
}
