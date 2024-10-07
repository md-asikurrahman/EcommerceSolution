using ECommerceSolution.WEB.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceSolution.WEB.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult LogIn()
        {

            return View();
        }
        [HttpPost]
        public IActionResult LogIn(LogInViewModel logInView)
        {
            return View();
        }
    }
}
