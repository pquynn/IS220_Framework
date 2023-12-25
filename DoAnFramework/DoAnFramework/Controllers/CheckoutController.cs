using DoAnFramework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAnFramework.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly book_shop_dbContext _context;

        public CheckoutController(book_shop_dbContext context)
        {
            _context = context;
        }

        //GET: Order/Index/"KH009"
        public IActionResult Index(string user_id = "KH009")
        {
            var order = _context.Orders
                .AsNoTracking()
                .Include(od => od.OrderDetails)
                    .ThenInclude(od => od.Book.BookImage) // Include the related Book and BookImage for each OrderDetail
                .FirstOrDefault(ud => ud.UserId == user_id && ud.Status == "cart");

            return View(order);
        }
    }
}
