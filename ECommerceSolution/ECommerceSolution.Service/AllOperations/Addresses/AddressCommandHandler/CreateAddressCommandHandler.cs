using MediatR;
using ECommerceSolution.Service.AllOperations.Addresses.AddressCommand;

namespace ECommerceSolution.Service.AllOperations.Addresses.AddressCommandHandler
{
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, IList<string>>
    {
        public async Task<IList<string>> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
