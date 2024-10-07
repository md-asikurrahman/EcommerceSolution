using MediatR;

namespace ECommerceSolution.Service.AllOperations.Addresses.AddressQuery
{
    public record class GetAddressByIdQuery(int Id) : IRequest<string>;

    public class GetAddressByIdQueryHandler : IRequestHandler<GetAddressByIdQuery, string>
    {
        public async Task<string> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}