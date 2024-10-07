using ECommerceSolution.WEB.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using ECommerceSolution.Service.AllOperations.Vendors.VendorCommand;

namespace ECommerceSolution.WEB.Areas.Admin.ViewModel.VendorModels
{
    public class UpdateVendorModel : BaseEntityVM
    {
        public string? VendorName { get; set; }
        public string? MobileNumber { get; set; }
        public string? Description { get; set; }
        public int VendorTypeId { get; set; }
        public int UserId { get; set; }
        public List<SelectListItem> VendorTypes { get; set; } = new();

        public UpdateVendorCommand PrepareCommand()
        {
            return new UpdateVendorCommand()
            {
                VendorName = VendorName,
                MobileNumber = MobileNumber,
                Description = Description,
                VendorTypeId = VendorTypeId,
                IsActive = IsActive,
                CreatedBy = CreatedBy,
                CreatedAt = CreatedAt,
                UpdatedBy = UpdatedBy,
                SerialNumber = SerialNumber

            };
        }
    }
}
