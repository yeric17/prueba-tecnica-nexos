using FluentValidation;

namespace Application.Orders.GetAllOrders
{
    public class GetAllOrdersQueryValidator : AbstractValidator<GetAllOrdersQuery>
    {
        public GetAllOrdersQueryValidator()
        {
            // No hay parámetros para validar en este query
        }
    }
}
