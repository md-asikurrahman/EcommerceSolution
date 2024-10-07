using MediatR;
using ECommerceSolution.Service.AllOperations.AddressTypes.AddressTypeCommand;

namespace ECommerceSolution.Service.AllOperations.AddressTypes.AddressTypeCommandHandler
{
    public class UpdateAddressTypeCommandHandler : IRequestHandler<UpdateAddressTypeCommand, IList<string>>
    {
        public async Task<IList<string>> Handle(UpdateAddressTypeCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
