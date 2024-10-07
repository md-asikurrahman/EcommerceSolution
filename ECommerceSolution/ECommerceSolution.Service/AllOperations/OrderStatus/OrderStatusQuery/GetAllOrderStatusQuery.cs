using MediatR;

namespace ECommerceSolution.Service.AllOperations.OrderStatus.OrderStatusQuery
{
    public record class GetAllOrderStatusQuery : IRequest<IList<string>>;

    public class GetAllOrderStatusQueryHandler : IRequestHandler<GetAllOrderStatusQuery, IList<string>>
    {
        public async Task<IList<string>> Handle(GetAllOrderStatusQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
