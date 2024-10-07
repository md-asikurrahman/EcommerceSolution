using ECommerceSolution.Service.AllOperations.Categories.CategoryCommand;
using ECommerceSolution.WEB.Areas.Admin.ViewModel.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerceSolution.WEB.Areas.Admin.ViewModel.CategoryModels
{
    public class UpdateCategoryModel : ViewBaseProperty
    {
        public int? ParentCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; } = string.Empty;
        public string ImageUrl { get; set; }

        public List<SelectListItem> ParentCategoryList = new();
        public UpdateCategoryCommand PrepareCommand()
        {
            return new UpdateCategoryCommand()
            {
                ParentCategoryId = ParentCategoryId,
                CategoryName = CategoryName,
                ImageUrl = ImageUrl,
                CategoryDescription = CategoryDescription,
                SerialNumber = SerialNumber,
                CreatedBy = CreatedBy,
                CreatedAt = CreatedAt,
                UpdatedBy = UpdatedBy,
                IsActive = IsActive
            };
        }
    }
}
