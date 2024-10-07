using MediatR;

namespace ECommerceSolution.Service.AllOperations.VendorTypes.VendorTypeCommand
{
    public class CreateVendorTypeCommand : BaseProperty, IRequest<List<string>>
    {
        public string TypeName { get; set; }
    }
}
