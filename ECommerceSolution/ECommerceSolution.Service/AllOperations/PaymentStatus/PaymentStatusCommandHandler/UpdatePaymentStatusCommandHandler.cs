using MediatR;
using ECommerceSolution.Service.AllOperations.PaymentStatus.PaymentStatusCommand;

namespace ECommerceSolution.Service.AllOperations.PaymentStatus.PaymentStatusCommandHandler
{
    public class UpdatePaymentStatusCommandHandler : IRequestHandler<UpdatePaymentStatusCommand, IList<string>>
    {
        public async Task<IList<string>> Handle(UpdatePaymentStatusCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
