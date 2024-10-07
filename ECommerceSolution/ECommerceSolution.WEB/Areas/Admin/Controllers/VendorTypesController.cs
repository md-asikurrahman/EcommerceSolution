using MediatR;
using Microsoft.AspNetCore.Mvc;
using ECommerceSolution.WEB.Areas.Admin.ViewModel.VendorTypeModels;
using ECommerceSolution.Service.AllOperations.VendorTypes.VendorTypeQuery;
using ECommerceSolution.Service.AllOperations.VendorTypes.VendorTypeQuery.GetAllVendorTypes;

namespace ECommerceSolution.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VendorTypesController : Controller
    {
        private readonly ISender _sender;
        public VendorTypesController(ISender sender)
        {
            _sender = sender;
        }
        public async Task<IActionResult> Index(string msg = null)
        {
            if (msg != null) 
            {
                ViewBag.msg = msg;
            }
            var result = await _sender.Send(new GetAllVendorTypesQuery());
            if (result != null && result.Count>0) 
            {
                var vendorTypeLists = new List<VendorTypeModel>();
                foreach (var vendorType in result) 
                {
                    var vendorType1 = new VendorTypeModel();
                    vendorType1.Id = vendorType.Id;
                    vendorType1.TypeName = vendorType.TypeName;
                    vendorType1.IsActive = vendorType.IsActive;
                    vendorType1.CreatedBy = vendorType.CreatedBy;
                    vendorType1.UpdatedBy = vendorType.UpdatedBy;
                    vendorType1.CreatedAt = vendorType.CreatedAt;
                    vendorType1.UpdatedAt = vendorType.UpdatedAt;
                    vendorTypeLists.Add(vendorType1);
                }

                return View(vendorTypeLists);
            }
            return View();
        }
        public IActionResult create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(IFormCollection formCollection)
        {
            var model = new CreateVendorTypeModel();

            model.TypeName = formCollection["TypeName"].ToString().Trim();
            var IsActive = formCollection["IsActive"].ToString().Trim();
            if (IsActive == "false")
            {
                model.IsActive = false;
            }
            else
            {
                model.IsActive = true;
            }

            try
            {
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
                                return RedirectToAction("Index", new { msg = $"Success: {errorMessage}" });
                            }
                            else
                            {
                                // Handle different error codes
                                return View(new { msg = $"Message: {errorMessage}" });
                            }
                        }
                        else
                        {
                            // Handle different error codes
                            return View(new { msg = $"Message: {item}" });
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

            var result = await _sender.Send(new GetVendorTypeByIdQuery(id));
            if (result != null)
            {
                var vendorType = new UpdateVendorTypeModel();
                vendorType.Id = result.Id;
                vendorType.TypeName = result.TypeName;
                vendorType.SerialNumber = result.SerialNumber;
                vendorType.IsActive = result.IsActive;
                vendorType.CreatedAt = result.CreatedAt;
                vendorType.CreatedBy = result.CreatedBy;
                vendorType.UpdatedBy = result.UpdatedBy;
                vendorType.UpdatedAt = result.UpdatedAt;

                return View(vendorType);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(IFormCollection formCollection)
        {
            var vendorType = new UpdateVendorTypeModel();
            vendorType.Id = int.Parse( formCollection["Id"].ToString());
            vendorType.TypeName = formCollection["TypeName"].ToString();
            vendorType.SerialNumber = int.Parse(formCollection["SerialNumber"].ToString());
            vendorType.IsActive = bool.Parse(formCollection["IsActive"].ToString());
            vendorType.CreatedAt = formCollection["CreatedAt"].ToString();
            vendorType.CreatedBy = formCollection["CreatedBy"].ToString();
            vendorType.UpdatedBy = formCollection["UpdatedBy"].ToString();
            vendorType.UpdatedAt = formCollection["UpdatedAt"].ToString();

            var result = await _sender.Send(vendorType.UpdateCommand());

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
                            return RedirectToAction("Index", new { msg = $"Success: {errorMessage}" });
                        }
                        else
                        {
                            // Handle different error codes
                            return View(new { msg = $"Message: {errorMessage}" });
                        }
                    }
                    else
                    {
                        // Handle different error codes
                        return View(new { msg = $"Message: {item}" });
                    }
                }
            }
            return View();
        }

        public async Task<IActionResult> Details(int id,string msg = null)
        {
            if (msg != null)
            {
                ViewBag.msg = msg;
            }
            if (id == 0)
            {
                return NotFound();
            }
            var result = await _sender.Send(new GetVendorTypeByIdQuery(id));
            if (result != null)
            {
                var vendorType = new UpdateVendorTypeModel();
                vendorType.Id = result.Id;
                vendorType.TypeName = result.TypeName;
                vendorType.SerialNumber = result.SerialNumber;
                vendorType.IsActive = result.IsActive;
                vendorType.CreatedAt = result.CreatedAt;
                vendorType.CreatedBy = result.CreatedBy;
                vendorType.UpdatedBy = result.UpdatedBy;
                vendorType.UpdatedAt = result.UpdatedAt;

                return View(vendorType);
            }
            return View();
        }
    }
}
