using ECommerceSolution.Service.AllOperations.VendorTypes.VendorTypeCommand;
using ECommerceSolution.WEB.ViewModels;

namespace ECommerceSolution.WEB.Areas.Admin.ViewModel.VendorTypeModels
{
    public class CreateVendorTypeModel : BaseEntityVM
    {
        public string TypeName { get; set; }

        public CreateVendorTypeCommand PrepareCommand()
        {
            return new CreateVendorTypeCommand()
            {
                TypeName = TypeName,
                CreatedBy = "CreatedBy",
                IsActive = true,
                IsDeleted = false,
                SerialNumber = SerialNumber
            };
        }
    }
}
