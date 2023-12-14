using Microsoft.AspNetCore.Mvc;
using DoAnFramework.Models;

namespace DoAnFramework.Controllers
{
    public class LoginController : Controller
    {
		book_shop_dbContext _context = new book_shop_dbContext();

		public IActionResult Index()
        {
            return View();
        }

		public IActionResult Login()
		{
            book_shop_dbContext context = _context;
             
			return View();
		}

		public IActionResult Register()
        {
            return View();
        }
    }
}
