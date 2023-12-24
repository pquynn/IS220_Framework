using DoAnFramework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAnFramework.Controllers
{
    public class BookController : Controller
    {
        private readonly book_shop_dbContext _context;

        public BookController(book_shop_dbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var listBook = _context.Books.ToList();
            return View(listBook);
        }

        public IActionResult BookDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _context.Books
                .Where(item => item.BookId == id)
                .Include(item => item.Comments)
                .FirstOrDefault();

            return View(product);
        }
    }
}
