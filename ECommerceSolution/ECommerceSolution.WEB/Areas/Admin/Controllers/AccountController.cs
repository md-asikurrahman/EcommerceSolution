using Microsoft.AspNetCore.Mvc;

namespace ECommerceSolution.WEB.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet, Route("login")]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User!.Identity!.IsAuthenticated)
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });

            var model = 
            returnUrl ??= Url.Content("~/");
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        [HttpPost]
        public IActionResult LogIn()
        {
            return View();
        }
    }
}
