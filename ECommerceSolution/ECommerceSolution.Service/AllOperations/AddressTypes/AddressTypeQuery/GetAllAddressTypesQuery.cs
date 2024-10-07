using MediatR;

namespace ECommerceSolution.Service.AllOperations.AddressTypes.AddressTypeQuery
{
    public record class GetAllAddressTypesQuery:IRequest<string>;

    public class GetAllAddressTypesQueryHandler : IRequestHandler<GetAllAddressTypesQuery, string>
    {
        public Task<string> Handle(GetAllAddressTypesQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
