using MediatR;

namespace ECommerceSolution.Service.AllOperations.VendorTypes.VendorTypeCommand
{
    public class UpdateVendorTypeCommand : BaseProperty, IRequest<List<string>>
    {
        public string TypeName { get; set; }
    }
}
