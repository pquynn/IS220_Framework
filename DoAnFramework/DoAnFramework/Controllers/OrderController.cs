using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoAnFramework.Models;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using DoAnFramework.Models.Service;
using AspNetCoreHero.ToastNotification.Abstractions;
using System.Xml.Linq;
using DoAnFramework.Models.ViewModel;

namespace DoAnFramework.Controllers
{
	public class OrderController : Controller
	{

        private readonly OrderService _orderService;
        private readonly CommentService _commentService;
        public OrderController(OrderService orderService, CommentService commentService)
        {
            _orderService = orderService;
            _commentService = commentService;
        }


        //GET: Order/order list/"KH009"
        public IActionResult Index(int page = 1) //my order
        {
            var user_id = HttpContext.Session.GetString("userId");
            var models = _orderService.GetMyOrderList(user_id, page, 20);
            ViewBag.CurrentPage = page;
            return View(models);

		}

		[HttpPost]
		public IActionResult addToCart(string userID, productData productData)
		{

			return Json(_orderService.addToCart(userID, productData));
		}
	


        //GET: Order/OrderDetail/3
        public IActionResult OrderDetail(int? id)
        {
            var order = _orderService.GetMyOrderDetails(id);
            if (order == null)
            {
                return RedirectToAction("Index", "Order");
            }

            return View(order);
        }


        //GET: Order/OrderFeedback/3
        public IActionResult OrderFeedback(int? id)
        {
            var order = _commentService.GetMyOrderWithComment(id);
            if (order == null)
            {
                return RedirectToAction("Index", "Order");
            }

            return View(order);
        }


        //GET: Order/Cart/"KH009"
        public IActionResult Cart(string user_id = "") //my order
        {
            var cartViewModel = _orderService.GetLoginCart(user_id);

            if (cartViewModel == null)
            {
                return View(); // Handle the case where the cart is null
            }

            return View(cartViewModel);
        }


        //POST: Order/UpdateCartProduct/id, quantity
        [HttpPost]
        public IActionResult UpdateCartProduct(int order_detail_id, int quantity) //my order
        {
            if (_orderService.UpdateOrderDetailQuantity(order_detail_id, quantity))
                return Json(new { success = true, message = "Success." });
            else
                return Json(new { success = false, message = "Failed." });
            
        }


        //POST: Order/DeleteOrderDetail/order_detail_id
        [HttpPost]
        public IActionResult DeleteOrderDetail(int orderDetailId)
        {
            if(_orderService.DeleteOrderDetail(orderDetailId))
                return Json(new { success = true, message = "Xóa sách thành công" });
            
           else
                return Json(new { success = false, message = "Xóa sách không thành công" });
            
        }


        //POST: Order/CancelOrder/orderId
        [HttpPost]
        public IActionResult CancelOrder(int orderId)
        {
            var success = _orderService.updateOrderStatus(orderId, "cancelled");

            return Json(success);
        }


        //POST: Order/AddFeedback/...
        [HttpPost]
        public IActionResult AddFeedback(string userId, string userName, List<CommentList> comments)
        {
            var success = _commentService.addFeedback(userId, userName, comments);

            return Json(success);
        }
    }

}
