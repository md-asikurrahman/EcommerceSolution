using MediatR;
using ECommerceSolution.Service.AllOperations.OrderStatus.OrderStatusCommand;

namespace ECommerceSolution.Service.AllOperations.OrderStatus.OrderStatusCommandHandler
{
    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand,IList<string>>
    {
        public Task<IList<string>> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
