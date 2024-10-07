namespace ECommerceSolution.WEB.Areas.Admin.ViewModel.VendorTypeModels
{
    public class VendorTypeModel
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public int SerialNumber { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string Status => IsActive ? "Active" : "InActive";
        public string StatusClass => IsActive ? "open-bt" : "close-bt";
    }
}
