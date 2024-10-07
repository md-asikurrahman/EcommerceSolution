using MediatR;

namespace ECommerceSolution.Service.AllOperations.Addresses.AddressQuery
{
    public record class GetAllAddressesQuery:IRequest<IList<string>>;

    public class GetAllAddressesQueryHandler : IRequestHandler<GetAllAddressesQuery, IList<string>>
    {
        public async Task<IList<string>> Handle(GetAllAddressesQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
