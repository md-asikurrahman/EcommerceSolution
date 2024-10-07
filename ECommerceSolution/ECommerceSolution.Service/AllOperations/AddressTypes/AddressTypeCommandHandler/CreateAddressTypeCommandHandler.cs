using MediatR;
using ECommerceSolution.Service.AllOperations.AddressTypes.AddressTypeCommand;

namespace ECommerceSolution.Service.AllOperations.AddressTypes.AddressTypeCommandHandler
{
    public class CreateAddressTypeCommandHandler : IRequestHandler<CreateAddressTypeCommand, IList<string>>
    {
        public async Task<IList<string>> Handle(CreateAddressTypeCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
