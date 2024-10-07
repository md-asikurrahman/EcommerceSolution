using MediatR;

namespace ECommerceSolution.Service.AllOperations.Addresses.AddressCommand
{
    public class CreateAddressCommand : BaseProperty, IRequest<IList<string>>
    {

    }
}
