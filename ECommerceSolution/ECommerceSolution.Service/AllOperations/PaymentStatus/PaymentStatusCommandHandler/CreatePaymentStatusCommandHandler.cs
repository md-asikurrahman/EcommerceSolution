
using ECommerceSolution.Service.AllOperations.PaymentStatus.PaymentStatusCommand;
using MediatR;

namespace ECommerceSolution.Service.AllOperations.PaymentStatus.PaymentStatusCommandHandler
{
    public class CreatePaymentStatusCommandHandler : IRequestHandler<CreatePaymentStatusCommand, IList<string>>
    {
        public Task<IList<string>> Handle(CreatePaymentStatusCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
