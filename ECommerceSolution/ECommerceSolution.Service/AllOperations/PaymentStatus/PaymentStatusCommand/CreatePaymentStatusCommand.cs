using MediatR;

namespace ECommerceSolution.Service.AllOperations.PaymentStatus.PaymentStatusCommand
{
    public class CreatePaymentStatusCommand : BaseProperty, IRequest<IList<string>>
    {
    }
}
