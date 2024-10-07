using MediatR;

namespace ECommerceSolution.Service.AllOperations.Vendors.VendorCommand
{
    public class UpdateVendorCommand : BaseProperty, IRequest<List<string>>
    {
        public string? VendorName { get; set; }
        public string? MobileNumber { get; set; }
        public string? Description { get; set; }
        public int VendorTypeId { get; set; }
        public int UserId { get; set; }
    }
}
