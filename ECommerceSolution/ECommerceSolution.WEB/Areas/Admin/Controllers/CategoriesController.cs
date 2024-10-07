using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ECommerceSolution.WEB.Areas.Admin.CommonMethods;
using ECommerceSolution.WEB.Areas.Admin.ViewModel.CategoryModels;
using ECommerceSolution.Service.AllOperations.Categories.CategoryQuery;


namespace ECommerceSolution.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly ISender _sender;
        private readonly IWebHostEnvironment _host;
        private readonly ImageUpload _imageUpload;
        // private readonly IFileStorageService _fileStorageService;
        public CategoriesController(ISender sender, IWebHostEnvironment host, ImageUpload imageUpload/*, IFileStorageService fileStorageService*/)
        {
            _sender = sender;
            _host = host;
            _imageUpload = imageUpload;
            //_fileStorageService = fileStorageService;
        }
        public async Task<IActionResult> Index(string msg = null)
        {
            if (msg != null)
            {
                ViewBag.Msg = msg;
            }
            var result = await _sender.Send(new GetAllCategoriesQuery());
            if (result != null && result.Count > 0)
            {
                var categories = new List<CategoryModel>();
                foreach (var item in result)
                {
                    var category = new CategoryModel()
                    {
                        Id = item.Id,
                        CategoryName = item.CategoryName,
                        ImageUrl = item.ImageUrl,
                        CategoryDescription = item.CategoryDescription,
                        IsActive = item.IsActive!,
                        CreatedAt = item.CreatedAt!,
                        CreatedBy = item.CreatedBy!
                    };
                    categories.Add(category);
                }
                return View(categories);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CreateCategoryModel category = new CreateCategoryModel();
            var result = _sender.Send(new GetAllCategoriesQuery());
            if (result.Result.Count > 0)
            {
                foreach (var item in result.Result)
                {
                    category.ParentCategoryList.Add(new SelectListItem
                    {
                        Text = item.CategoryName,
                        Value = item.Id.ToString()
                    });
                }
            }

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormCollection formCollection)
        {
            CreateCategoryModel category = new CreateCategoryModel();
            var imageFileName = "";
            if (formCollection.Files.Count != 0)
            {

                var file = formCollection.Files[0];
                var folderName = "Images/CategoryImage/";
                var imageName = "Category_";

                imageFileName = await _imageUpload.UploadFileAsync(file, folderName, imageName);
            }
            category.ParentCategoryId = int.Parse(formCollection["ParentCategoryId"].ToString());
            category.CategoryName = formCollection["CategoryName"].ToString();
            category.CategoryDescription = formCollection["CategoryDescription"].ToString();
            var isActive = formCollection["IsActive"].ToString();
            category.ImageUrl = imageFileName;
            if (isActive == "false")
            {
                category.IsActive = false;
            }
            else
            {
                category.IsActive = true;
            }
            try
            {
                var result = await _sender.Send(category.PrepareCommand());

                if (result.Count > 0)
                {
                    foreach (var item in result)
                    {
                        // Split the item by ", " to separate "Error Code" and "Error Message"
                        var parts = item.Split(new[] { ", " }, StringSplitOptions.None);

                        if (parts.Length == 2)
                        {
                            // Extract "Error Code" from the first part
                            var errorCodePart = parts[0].Split(new[] { ": " }, StringSplitOptions.None);
                            string errorCode = errorCodePart.Length == 2 ? errorCodePart[1].Trim() : string.Empty;

                            // Extract "Error Message" from the second part
                            var errorMessagePart = parts[1].Split(new[] { ": " }, StringSplitOptions.None);
                            string errorMessage = errorMessagePart.Length == 2 ? errorMessagePart[1].Trim() : string.Empty;

                            // Now you can check the Error Code and handle accordingly
                            if (errorCode == "00")
                            {
                                // Success case
                                return RedirectToAction("Index", new { msg = errorMessage });
                            }
                            else
                            {
                                // Handle different error codes
                                return View(new { msg = errorMessage });
                            }
                        }
                        else
                        {
                            // Handle different error codes
                            return View(new { msg = item });
                        }
                    }
                }
                return View();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, string msg = null)
        {
            if (msg != null)
            {
                ViewBag.msg = msg;
            }
            if (id == 0)
            {
                return NotFound();
            }

            UpdateCategoryModel model = new UpdateCategoryModel();
            var result = await _sender.Send(new GetCategoryByIdQuery(id));
            if (result != null)
            {
                model.CategoryName = result.CategoryName;
                model.CategoryDescription = result.CategoryDescription;
                model.SerialNumber = result.SerialNumber;
                model.ImageUrl = $"/Images/CategoryImage/{result.ImageUrl}";
                model.IsActive = result.IsActive;
                model.ParentCategoryId = result.ParentCategoryId;
                model.CreatedAt = result.CreatedAt!;
                model.CreatedBy = result.CreatedBy!;
                model.UpdatedBy = result.UpdatedBy!;
                model.UpdatedAt = result.UpdatedAt!;
            }
            var parentCategory = await _sender.Send(new GetAllCategoriesQuery());

            if (parentCategory.Count > 0)
            {
                foreach (var item in parentCategory)
                {
                    model.ParentCategoryList.Add(new SelectListItem
                    {
                        Text = item.CategoryName,
                        Value = item.Id.ToString(),
                        Selected = item.Id == model.ParentCategoryId
                    });
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(IFormCollection formCollection)
        {
            UpdateCategoryModel model = new UpdateCategoryModel();
            var imageFileName = "";
            if (formCollection.Files.Count != 0)
            {
                var file = formCollection.Files[0];
                var folderName = "Images/CategoryImage/";
                var imageName = "Category_";

                imageFileName = await _imageUpload.UploadFileAsync(file, folderName, imageName);

                // Update your model or perform other actions with the file name
                model.ImageUrl = imageFileName;
            }
            else
            {
                model.ImageUrl = formCollection["ImageUrl"].ToString();
            }
            var isActive = formCollection["IsActive"].ToString();
            if (isActive == "false")
            {
                model.IsActive = false;
            }
            else
            {
                model.IsActive = true;
            }
            model.Id = int.Parse(formCollection["SliderImage"].ToString());
            model.ParentCategoryId = int.Parse(formCollection["ParentCategoryId"].ToString());
            model.SerialNumber = int.Parse(formCollection["SerialNumber"].ToString());
            model.CategoryName = formCollection["CategoryName"].ToString();
            model.CategoryDescription = formCollection["CategoryDescription"].ToString();
            model.CreatedAt = formCollection["CreatedAt"].ToString();
            model.CreatedBy = formCollection["CreatedBy"].ToString();
            model.UpdatedBy = "User";

            var result = await _sender.Send(model.PrepareCommand());

            if (result.Count > 0)
            {
                foreach (var item in result)
                {
                    // Split the item by ", " to separate "Error Code" and "Error Message"
                    var parts = item.Split(new[] { ", " }, StringSplitOptions.None);

                    if (parts.Length == 2)
                    {
                        // Extract "Error Code" from the first part
                        var errorCodePart = parts[0].Split(new[] { ": " }, StringSplitOptions.None);
                        string errorCode = errorCodePart.Length == 2 ? errorCodePart[1].Trim() : string.Empty;

                        // Extract "Error Message" from the second part
                        var errorMessagePart = parts[1].Split(new[] { ": " }, StringSplitOptions.None);
                        string errorMessage = errorMessagePart.Length == 2 ? errorMessagePart[1].Trim() : string.Empty;

                        // Now you can check the Error Code and handle accordingly
                        if (errorCode == "00")
                        {
                            // Success case
                            return RedirectToAction("Index", new { msg = errorMessage });
                        }
                        else
                        {
                            // Handle different error codes
                            return View(new { msg = errorMessage });
                        }
                    }
                    else
                    {
                        // Handle different error codes
                        return View(new { msg = item });
                    }
                }
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = new CategoryModel();
            var childList = new List<CategoryModel>();
            var categoryDetails = await _sender.Send(new GetCategoryByIdQuery(id));
            if (categoryDetails != null)
            {
                model.Id = categoryDetails.Id;
                model.CategoryName = categoryDetails.CategoryName;
                model.CategoryDescription = categoryDetails.CategoryDescription;
                model.ImageUrl = categoryDetails.ImageUrl;
                model.IsActive = categoryDetails.IsActive;

            }
            var childCategory = _sender.Send(new GetChildByParentCategoryIdQuery(categoryDetails!.ParentCategoryId!));
            if (childCategory != null & childCategory!.Result.Count > 0)
            {
                foreach (var item in childCategory!.Result)
                {
                    var childModel = new CategoryModel();
                    childModel.Id = item.Id;
                    childModel.CategoryName = item.CategoryName;
                    childModel.CategoryDescription = item.CategoryDescription;
                    childModel.ImageUrl = item.ImageUrl;
                    childModel.IsActive = item.IsActive;

                    childList.Add(childModel);
                }
            }
            model.ChildCategories = childList;
            return View(model);
        }
    }
}
