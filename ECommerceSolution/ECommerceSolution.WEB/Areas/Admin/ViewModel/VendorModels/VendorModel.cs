using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerceSolution.WEB.Areas.Admin.ViewModel.VendorModels
{
    public class VendorModel
    {
        public int Id { get; set; }
        public string? VendorName { get; set; }
        public string? MobileNumber { get; set; }
        public string? Description { get; set; }
        public int VendorTypeId { get; set; }
        public int SerialNumber { get; set; }
        public string VendorTypeName { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public string Status => IsActive ? "Active" : "InActive";
        public string StatusClass => IsActive ? "open-bt" : "close-bt";
        public List<SelectListItem> VendorTypes { get; set; } = new();
       
    }
}
