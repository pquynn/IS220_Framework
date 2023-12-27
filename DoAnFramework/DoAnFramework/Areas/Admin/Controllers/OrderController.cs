using DoAnFramework.Models;
using DoAnFramework.Models.Service;
using Microsoft.AspNetCore.Mvc;

namespace DoAnFramework.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        //GET: Admin/Order/order list/
        public IActionResult Index(int page = 1) 
        {
            var models = _orderService.GetOrderList(page, 10);
            ViewBag.CurrentPage = page;
            return View(models);
        }

        //GET: Admin/Order/OrderDetail/3
        public IActionResult OrderDetail(int? id)
        {
            var order = _orderService.GetMyOrderDetails(id);
            if (order == null)
            {
                return RedirectToAction("Index", "Order");
            }

            return View(order);
        }

        //POST: Admin/Order/updateorderstatus/orderId
        [HttpPost]
        public IActionResult UpdateOrderStatus(int orderId, string status)
        {
            var success = _orderService.updateOrderStatus(orderId, status);

            return Json(success);
        }

        //POST: Admin/Order/DeleteOrder/orderId
        [HttpPost]
        public IActionResult DeleteOrder(int orderId)
        {
            var success = _orderService.deleteOrder(orderId);

            return Json(success);
        }

    }

    
}
