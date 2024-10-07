using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ECommerceSolution.WEB.Areas.Admin.ViewModel.VendorModels;
using ECommerceSolution.Service.AllOperations.Vendors.VendorQuery;
using ECommerceSolution.Service.AllOperations.VendorTypes.VendorTypeQuery.GetAllVendorTypes;

namespace ECommerceSolution.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VendorsController : Controller
    {
        private readonly ISender _sender;
        public VendorsController(ISender sender)
        {
            _sender = sender;
        }
        public async Task<IActionResult> Index(string msg = null)
        {
            if (msg != null)
            {
                ViewBag.Msg = msg;
            }
            var result = await _sender.Send(new GetAllVendorsQuery());
            if (result != null && result.Count >0)
            {
                var vendors = new List<VendorModel>();
                foreach (var item in result)
                {
                    var vendor = new VendorModel()
                    {
                        Id = item.Id,
                        VendorName = item.VendorName,
                        VendorTypeId = item.VendorTypeId,
                        VendorTypeName = item.VendorTypeName!,
                        Description = item.Description,
                        MobileNumber = item.MobileNumber,
                        IsActive = item.IsActive,
                        CreatedAt = item.CreatedAt!,
                        CreatedBy = item.CreatedBy!
                    };
                    vendors.Add(vendor);
                }
                return View(vendors);
            }
            return View();
        }
        [HttpGet]
        public async Task< IActionResult> Create()
        {
            VendorModel vendor = new VendorModel();
            var result = _sender.Send(new GetAllVendorTypesQuery());
            if (result.Result.Count>0)
            {
                foreach (var item in result.Result)
                {
                    vendor.VendorTypes.Add(new SelectListItem
                    {
                        Text = item.TypeName,
                        Value = item.Id.ToString()
                    });
                }
            }
           
            return View(vendor);
        }
        [HttpPost]
        public async Task<IActionResult> Create(IFormCollection formCollection)
        {
            var model = new CreateVendorModel();

            model.VendorName = formCollection["VendorName"].ToString().Trim();
            model.Description = formCollection["Description"].ToString().Trim();
            model.MobileNumber = formCollection["MobileNumber"].ToString().Trim();
            model.VendorTypeId = int.Parse(formCollection["VendorTypeId"].ToString().Trim());
            var IsActive = formCollection["IsActive"].ToString().Trim();
            if (IsActive == "false")
            {
                model.IsActive = false;
            }
            else
            {
                model.IsActive= true;
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
            UpdateVendorModel vendor = new UpdateVendorModel();
            if (msg != null)
            {
                ViewBag.msg = msg;
            }
            if (id == 0)
            {
                return NotFound();
            }
            var result = await _sender.Send(new GetVendorByIdQuery(id));
            if (result != null)
            {
                vendor.Id = result.Id;
                vendor.MobileNumber = result.MobileNumber;
                vendor.VendorName = result.VendorName;
                vendor.IsActive = result.IsActive;
                vendor.UpdatedBy = result.UpdatedBy;
                vendor.UpdatedAt = result.UpdatedAt;
                vendor.CreatedAt = result.CreatedAt!;
                vendor.Description = result.Description;
                vendor.CreatedBy = result.CreatedBy!;
                vendor.VendorTypeId = result.VendorTypeId;
            }
            var vendorTypes = await _sender.Send(new GetAllVendorTypesQuery());
            if (vendorTypes.Count>0)
            {
                foreach (var item in vendorTypes)
                {
                    vendor.VendorTypes.Add(new SelectListItem
                    {
                        Text = item.TypeName,
                        Value = item.Id.ToString(),
                        Selected = item.Id == vendor.VendorTypeId
                    });
                }
            }
           
            return View(vendor);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(IFormCollection formCollection)
        {
            var model = new UpdateVendorModel();

            model.VendorName = formCollection["VendorName"].ToString().Trim();
            model.Description = formCollection["Description"].ToString().Trim();
            model.MobileNumber = formCollection["MobileNumber"].ToString().Trim();
            model.SerialNumber = int.Parse(formCollection["SerialNumber"].ToString().Trim());
            model.VendorTypeId = int.Parse(formCollection["VendorTypeId"].ToString().Trim());
            model.CreatedAt =formCollection["CreatedAt"].ToString().Trim();
            model.CreatedBy = formCollection["CreatedBy"].ToString().Trim();
            model.UpdatedBy = formCollection["UpdatedBy"].ToString().Trim();
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
                                return RedirectToAction("Index", new { msg =  errorMessage });
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
    }

}
