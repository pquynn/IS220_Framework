using DoAnFramework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAnFramework.Controllers
{
    public class BookController : Controller
    {
        //private readonly book_shop_dbContext _context;

        //public BookController(book_shop_dbContext context)
        //{
        //    _context = context;
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BookDetail()
        {
            //var product = _context.Books.Include(x=>x.Name).FirstOrDefault(x=>x.BookId == id);
            //if(product == null)
            //{
            //    return RedirectToAction("Index");
            //}
            return View();
        }
    }
}
