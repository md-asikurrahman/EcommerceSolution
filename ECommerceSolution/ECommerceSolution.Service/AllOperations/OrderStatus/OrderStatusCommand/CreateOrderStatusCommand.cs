using MediatR;

namespace ECommerceSolution.Service.AllOperations.OrderStatus.OrderStatusCommand
{
    public class CreateOrderStatusCommand : BaseProperty, IRequest<IList<string>>
    {
    }
}
