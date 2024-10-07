using MediatR;

namespace ECommerceSolution.Service.AllOperations.OrderStatus.OrderStatusCommand
{
    public class UpdateOrderStatusCommand : BaseProperty, IRequest<IList<string>>
    {
    }
}
