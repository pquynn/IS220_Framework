using DoAnFramework.Models;
using DoAnFramework.Models.ViewModel;
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


        //public IActionResult Index()
        //{

        //    var listBook = _context.Books
        //        .OrderByDescending(item => item.Name)
        //        .Include(item => item.BookImage)
        //        .Take(4)
        //        .ToList();

        //    var listBookNew = _context.Books
        //        .OrderBy(item => item.Name)
        //        .Include(item => item.BookImage)
        //        .Take(4)
        //        .ToList();



        //    var listBlog = _context.Blogs
        //        .OrderBy(item => item.BlogId)
        //        .Take(3)
        //        .ToList();

        //    var homepageDate = new HomepageProduct_Blog(listBook, listBlog, listBookNew);

        //    return View(homepageDate);
        //}

        public IActionResult Index()
        {
            var listBook = _context.Books
                .OrderByDescending(item => item.Name)
                .Take(8)
                .Select(item => new BookThumbnail
                {
                    Name = item.Name, // Replace with the actual property name
                    Price = item.Price,       // Replace with the actual property name
                    BookId = item.BookId,     // Replace with the actual property name
                    FrontCover = item.BookImage.FrontCover // Replace with the actual property name
                })
                .ToList();

            var listBookNew = _context.Books
                .OrderBy(item => item.Name)
                .Take(4)
                .Select(item => new BookThumbnail
                {
                    Name = item.Name, // Replace with the actual property name
                    Price = item.Price,       // Replace with the actual property name
                    BookId = item.BookId,     // Replace with the actual property name
                    FrontCover = item.BookImage.FrontCover // Replace with the actual property name
                })
                .ToList();

            var listBlog = _context.Blogs
                .OrderBy(item => item.BlogId)
                .Take(3)
                .Select(item=> new Blog 
                { 
                    BlogId = item.BlogId ,
                    BlogImage = item.BlogImage,
                    BlogTitle = item.BlogTitle,
                    Content = item.Content
                })
                .ToList();

            var homepageData = new HomepageProduct_Blog(listBook, listBlog, listBookNew);

            return View(homepageData);
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