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
        public async Task<IActionResult> Index(int? page = 1, string user_id = "KH009") //my order
        {
            var pageNumber = page == null || page < 0 ? 1 : page.Value;
            //var pageSize = Utilities.PAGE_SIZE;
            var pageSize = 20;
            var lsOrders = _context.Orders
             .AsNoTracking()
            .Where(ud => ud.UserId == user_id)
            .OrderByDescending(x => x.OrderId);
            PagedList<Order> models = new  PagedList<Order>(lsOrders, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;

            //var order = await _context.Orders
            //.Where(ud => ud.UserId == user_id)
            //.ToListAsync();
            //return View(order);

            return View(models);

        }

        //GET: Order/OrderDetail/3
        public IActionResult OrderDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var booK_shop_dbContext = _context.Orders.Include(x => x.OrderDetails);
            //var booK_shop_dbContext = _context.OrderDetails.Include(x => x.Order);
            var orderDetails = _context.OrderDetails
               .Include(x => x.Order)
               .Where(od => od.OrderId == id)
               .ToList();
            return View(orderDetails);
        }

        //public async Task<IActionResult> OrderDetail(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    //var booK_shop_dbContext = _context.Orders.Include(x => x.OrderDetails);
        //    //var booK_shop_dbContext = _context.OrderDetails.Include(x => x.Order);
        //    var orderDetails = await _context.OrderDetails
        //       .Include(x => x.Order)
        //       .Where(od => od.OrderId == id)
        //       .ToListAsync();
        //    return View(orderDetails);
        //}

        //GET: Order/OrderFeedback/3
        public IActionResult OrderFeedback()
        {
            return View();
        }

        //GET: Order/Cart/"KH009"
        public async Task<IActionResult> Cart(string user_id = "KH009") //my order
        {
            //var pageNumber = page == null || page < 0 ? 1 : page.Value;
            //var pageSize = Utilities.PAGE_SIZE;
            //var pageSize = 20;
            //var lsOrders = _context.Orders
            // .AsNoTracking()
            //.Where(ud => ud.UserId == user_id)
            //.OrderByDescending(x => x.OrderId);

            //PagedList<Order> models = new PagedList<Order>(lsOrders, pageNumber, pageSize);
            //ViewBag.CurrentPage = pageNumber;
            if (user_id == null)
            {
                return NotFound(); //xu ly sau
            }

            var cart = await _context.Orders
                .Where(ud => ud.UserId == user_id && ud.Status == "Đang mua hàng")
                .ToListAsync();
            return View(cart);

        }

    }
}
