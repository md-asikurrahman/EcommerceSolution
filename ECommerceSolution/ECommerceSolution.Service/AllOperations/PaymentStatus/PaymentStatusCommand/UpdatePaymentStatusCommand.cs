using MediatR;

namespace ECommerceSolution.Service.AllOperations.PaymentStatus.PaymentStatusCommand
{
    public class UpdatePaymentStatusCommand : BaseProperty, IRequest<IList<string>>
    {
    }
}
