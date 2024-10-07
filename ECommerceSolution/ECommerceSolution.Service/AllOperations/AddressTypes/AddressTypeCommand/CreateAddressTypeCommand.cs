using MediatR;

namespace ECommerceSolution.Service.AllOperations.AddressTypes.AddressTypeCommand
{
    public class CreateAddressTypeCommand : BaseProperty, IRequest<IList<string>>
    {
    }
}
