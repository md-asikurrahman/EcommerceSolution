using MediatR;

namespace ECommerceSolution.Service.AllOperations.Addresses.AddressCommand
{
    public class UpdateAddressCommand : BaseProperty, IRequest<IList<string>>
    {
    }
}
