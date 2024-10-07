using Microsoft.AspNetCore.Mvc.Rendering;
using ECommerceSolution.WEB.Areas.Admin.ViewModel.Common;

namespace ECommerceSolution.WEB.Areas.Admin.ViewModel.CategoryModels
{
    public class CategoryModel : ViewBaseProperty
    {       
        public int? ParentId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string ImageUrl {  get; set; }

        public List<SelectListItem> CategoryList = new();
        public IList<CategoryModel> ChildCategories { get; set; }
        public List<CategoryModel> MenuChildCategories { get; set; } = new List<CategoryModel>();
    }
}
