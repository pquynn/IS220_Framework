using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoAnFramework.Models;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;

namespace DoAnFramework.Controllers
{
    public class OrderController : Controller
    {

        private readonly book_shop_dbContext _context;

        public OrderController(book_shop_dbContext context)
        {
            _context = context;
        }

        //GET: Order/order list/"KH009"
        public IActionResult Index(int? page = 1, string user_id = "KH009") //my order
        {
            var pageNumber = page == null || page < 0 ? 1 : page.Value;
            //var pageSize = Utilities.PAGE_SIZE;
            var pageSize = 20;
            var lsOrders = _context.Orders
            .AsNoTracking()
            .Where(ud => ud.UserId == user_id && ud.Status != "Đang mua hàng")
            .OrderByDescending(x => x.OrderId);
            PagedList<Order> models = new (lsOrders, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;


            return View(models);

        }

        //GET: Order/OrderDetail/3
        public IActionResult OrderDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = _context.Orders
                .AsNoTracking()
                .Where(od => od.OrderId == id)
                .Include(x => x.OrderDetails)
                    .ThenInclude(od => od.Book) // Include the related Book for each OrderDetail
                        .ThenInclude(od => od.BookImage)
                .FirstOrDefault();
            return View(order);
        }

        //GET: Order/OrderFeedback/3
        public IActionResult OrderFeedback()
        {
            return View();
        }

        //GET: Order/Cart/"KH009"
        public IActionResult Cart(string user_id = "KH009") //my order
        {
            if (user_id == null)
            {
                return NotFound(); //xu ly sau
            }

            var cart = _context.Orders
                .AsNoTracking()
                .Where(ud => ud.UserId == user_id && ud.Status == "Đang mua hàng")
                .Include(x => x.OrderDetails)
                    .ThenInclude(od => od.Book) // Include the related Book for each OrderDetail
                        .ThenInclude(od => od.BookImage)
                .FirstOrDefault();
            return View(cart);

        }

    }
}
