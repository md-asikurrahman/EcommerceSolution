using MediatR;

namespace ECommerceSolution.Service.AllOperations.AddressTypes.AddressTypeQuery
{
    public record class GetAddressTypeByIdQuery(int Id):IRequest<string>;

    public class GetAddressTypeByIdQueryHandler : IRequestHandler<GetAddressTypeByIdQuery, string>
    {
        public async Task<string> Handle(GetAddressTypeByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
