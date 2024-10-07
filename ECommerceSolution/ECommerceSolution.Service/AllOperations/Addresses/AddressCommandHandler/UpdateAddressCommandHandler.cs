using MediatR;
using ECommerceSolution.Service.AllOperations.Addresses.AddressCommand;

namespace ECommerceSolution.Service.AllOperations.Addresses.AddressCommandHandler
{
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, IList<string>>
    {
        public async Task<IList<string>> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
