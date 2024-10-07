using MediatR;
using Microsoft.AspNetCore.Mvc;
using ECommerceSolution.WEB.Areas.Admin.ViewModel.CategoryModels;
using ECommerceSolution.Service.AllOperations.Categories.CategoryQuery;

namespace ECommerceSolution.WEB.Components
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly ISender _sender;
        public CategoryMenuViewComponent(ISender sender)
        {
            _sender = sender;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            CategoryModel category = new CategoryModel();
            List<CategoryModel> categoryList = new List<CategoryModel>();


            var result = await _sender.Send(new GetAllCategoriesQuery());
            foreach (var item in result)
            {
                var cate = new CategoryModel()
                {
                    CategoryName = item.CategoryName,
                    Id = item.Id,
                    ParentId = item.ParentCategoryId,
                    ImageUrl = item.ImageUrl
                };
                categoryList.Add(cate);
            }

            var model = BuildMenuTree(categoryList);

            return View(model);
        }

        private List<CategoryModel> BuildMenuTree(List<CategoryModel> menuLists)
        {
            var menuMap = new Dictionary<int, CategoryModel>();
            var rootMenu = new List<CategoryModel>();
            //for menu with Id//
            foreach (var menuItem in menuLists)
            {
                menuMap[menuItem.Id] = menuItem;

            }

            //for Menu///
            foreach (var parentMenu in menuLists)
            {
                //for pRENT MENU//
                if (parentMenu.ParentId == 0)
                {
                    rootMenu.Add(parentMenu);
                }
                else
                {
                    if (menuMap.TryGetValue(parentMenu.ParentId!.Value, out var parentMenuItem))
                    {
                        parentMenuItem.MenuChildCategories.Add(parentMenu);
                    }
                }
            }
            return rootMenu;
        }
    }
}
