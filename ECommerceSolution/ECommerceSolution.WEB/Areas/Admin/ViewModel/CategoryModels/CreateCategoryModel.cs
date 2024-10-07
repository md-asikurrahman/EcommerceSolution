using Microsoft.AspNetCore.Mvc.Rendering;
using ECommerceSolution.WEB.Areas.Admin.ViewModel.Common;
using ECommerceSolution.Service.AllOperations.Categories.CategoryCommand;

namespace ECommerceSolution.WEB.Areas.Admin.ViewModel.CategoryModels
{
    public class CreateCategoryModel : ViewBaseProperty
    {
        public int? ParentCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; } = string.Empty;
        public string ImageUrl { get; set; }
        public List<SelectListItem> ParentCategoryList = new();
        public CreateCategoryCommand PrepareCommand()
        {
            return new CreateCategoryCommand()
            {
                 ParentCategoryId = ParentCategoryId,
                 CategoryName = CategoryName,
                 ImageUrl = ImageUrl,
                 CategoryDescription = CategoryDescription,
                 SerialNumber = 0,
                 CreatedBy = "CreatedBy",
                 IsActive = IsActive
            };
        }
    }
}
