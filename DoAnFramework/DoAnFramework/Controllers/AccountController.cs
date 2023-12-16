using Microsoft.AspNetCore.Mvc;

namespace DoAnFramework.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index() //account profile
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult ForgetPassword()
        {
            return View(); 
        }

        public IActionResult ForgetPassword2()
        {
            return View();
        }
        public IActionResult LogOut()
        {
            return View();
        }

    }
}
