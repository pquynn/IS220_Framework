using Microsoft.AspNetCore.Mvc;

namespace DoAnFramework.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
