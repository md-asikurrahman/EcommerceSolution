using MediatR;

namespace ECommerceSolution.Service.AllOperations.AddressTypes.AddressTypeCommand
{
    public class UpdateAddressTypeCommand : BaseProperty, IRequest<IList<string>>
    {
    }
}
