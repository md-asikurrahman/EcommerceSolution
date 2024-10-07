using ECommerceSolution.Service.AllOperations.VendorTypes.VendorTypeCommand;

namespace ECommerceSolution.WEB.Areas.Admin.ViewModel.VendorTypeModels
{
    public class UpdateVendorTypeModel
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public int SerialNumber { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }

        public  UpdateVendorTypeCommand UpdateCommand()
        {
            return new UpdateVendorTypeCommand()
            {
                Id = Id,
                TypeName = TypeName,
                SerialNumber = SerialNumber,
                IsActive = IsActive,
                CreatedBy = CreatedBy,
                UpdatedBy = UpdatedBy,
                UpdatedAt = UpdatedAt,
                CreatedAt = CreatedAt
            };
        }
    }
}
