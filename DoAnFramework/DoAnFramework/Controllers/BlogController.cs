using Microsoft.AspNetCore.Mvc;


namespace DoAnFramework.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BlogDetail()
        {
            return View();
        }
    }
}
