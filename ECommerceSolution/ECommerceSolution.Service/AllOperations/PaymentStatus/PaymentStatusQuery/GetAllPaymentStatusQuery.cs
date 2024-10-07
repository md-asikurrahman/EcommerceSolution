using MediatR;

namespace ECommerceSolution.Service.AllOperations.PaymentStatus.PaymentStatusQuery
{
    public record class GetAllPaymentStatusQuery : IRequest<IList<string>>;

    public class GetAllPaymentStatusQueryHandler : IRequestHandler<GetAllPaymentStatusQuery, IList<string>>
    {
        public async Task<IList<string>> Handle(GetAllPaymentStatusQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
