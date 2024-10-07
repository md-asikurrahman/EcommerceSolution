using MediatR;
using ECommerceSolution.Service.AllOperations.OrderStatus.OrderStatusCommand;

namespace ECommerceSolution.Service.AllOperations.OrderStatus.OrderStatusCommandHandler
{
    public class CreateOrderStatusCommandHandler : IRequestHandler<CreateOrderStatusCommand, IList<string>>
    {
        public async Task<IList<string>> Handle(CreateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
