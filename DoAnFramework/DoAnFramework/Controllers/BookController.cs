using DoAnFramework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Infrastructure;
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
            var listBook = _context.Books
                .Include(item => item.BookImage)
                .ToList();
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
                .Include(item => item.BookImage)
                .Include(item => item.Category)
                .FirstOrDefault();

            return View(product);
        }

        public IActionResult BookCategory(int? id)
        {
            var listProduct = _context.Books
                .Where(item => item.CategoryId == id)
                .Include(item => item.BookImage)
                .ToList();

            return View(listProduct);
        }

        public IActionResult BookCover(int? typeCover)
        {
            var listProduct = _context.Books
                .Where(item => item.BookCover == typeCover)
                .Include(item => item.BookImage)
                .ToList();

            return View(listProduct);
        }
    }
}
