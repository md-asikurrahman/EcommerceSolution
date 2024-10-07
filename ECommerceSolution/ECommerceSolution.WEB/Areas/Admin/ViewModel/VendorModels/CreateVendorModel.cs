using ECommerceSolution.WEB.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using ECommerceSolution.Service.AllOperations.Vendors.VendorCommand;

namespace ECommerceSolution.WEB.Areas.Admin.ViewModel.VendorModels
{
    public class CreateVendorModel : BaseEntityVM
    {
        public string? VendorName { get; set; }
        public string? MobileNumber { get; set; }
        public string? Description { get; set; }
        public int VendorTypeId { get; set; }
        public int UserId { get; set; }
        public List<SelectListItem> VendorTypes { get; set; } = new();

        public CreateVendorCommand PrepareCommand()
        {
            return new CreateVendorCommand()
            {
                VendorName = VendorName,
                MobileNumber = MobileNumber,
                Description = Description,
                VendorTypeId = VendorTypeId,
                UserId = UserId,
                IsActive = IsActive,
                CreatedBy = "CreatedBy"
              
            };
        }
    }
}
