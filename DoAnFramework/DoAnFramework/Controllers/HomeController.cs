using DoAnFramework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DoAnFramework.Controllers
{
    public class HomeController : Controller
    {
        private readonly book_shop_dbContext _context;

        public HomeController(book_shop_dbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var listBook = _context.Books
                .OrderByDescending(item => item.Name)
                .Include(item => item.BookImage)
                .Take(4)
                .ToList();

            var listBookNew = _context.Books
                .OrderBy(item => item.Name)
                .Include(item => item.BookImage)
                .Take(4)
                .ToList();

            var listBlog = _context.Blogs
                .OrderBy(item => item.BlogId)
                .Take(3)
                .ToList();

            var homepageDate = new HomepageProduct_Blog(listBook, listBlog, listBookNew);
            
            return View(homepageDate);
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult question_page_Huy_don_hang()
        {
            return View();
        }

        public IActionResult question_page_Doi_tac_van_chuyen()
        {
            return View();
        }

        public IActionResult question_page_Phi_van_chuyen()
        {
            return View();
        }

        public IActionResult question_page_Phuong_thuc_thanh_toan()
        {
            return View();
        }

        public IActionResult Help()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}