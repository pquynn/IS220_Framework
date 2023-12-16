using Microsoft.AspNetCore.Mvc;

namespace DoAnFramework.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index() //my order
        {
            return View();
        }

        public IActionResult OrderDetail()
        {
            return View();
        }

        public IActionResult OrderFeedback()
        {
            return View();
        }

        public IActionResult Cart()
        {
            return View();
        }


    }
}
