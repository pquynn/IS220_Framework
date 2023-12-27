using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoAnFramework.Models;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;

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

			PagedList<Order> models = new PagedList<Order>(lsOrders, pageNumber, pageSize);
			ViewBag.CurrentPage = pageNumber;

			return View(models);

		}

		//GET: Order/OrderDetail/3
		public async Task<IActionResult> OrderDetail(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			//var booK_shop_dbContext = _context.Orders.Include(x => x.OrderDetails);
			//var booK_shop_dbContext = _context.OrderDetails.Include(x => x.Order);
			var orderDetails = await _context.OrderDetails
			   .Include(x => x.Order)
			   .Where(od => od.OrderId == id)
			   .ToListAsync();
			return View(orderDetails);
		}

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

		[HttpPost]
		public IActionResult addToCart(string? userID, [FromBody] JObject productData)
		{
			// Truy cập thông tin sản phẩm từ JObject
			string? productName = productData.GetValue("productName")?.ToString();
			int productID = productData.GetValue("productID")?.ToObject<int>() ?? 0;
			int productPrice = productData.GetValue("productPrice")?.ToObject<int>() ?? 0;
			int numberOfProduct = productData.GetValue("numberOfProduct")?.ToObject<int>() ?? 0;

			var orderID = _context.Orders.Where(item => item.UserId == userID).FirstOrDefault();

			if (orderID == null)
			{
				// Trường hợp chưa có giỏ hàng trước đó.
				var newOrder = new Order
				{
					UserId = userID,
					Status = "cart"
				};
				_context.Orders.Add(newOrder);

				orderID = _context.Orders.Where(item => item.UserId == userID && item.Status == "cart").FirstOrDefault();
			}
			var newProduct = new OrderDetail { 
				OrderId = orderID.OrderId, 
				BookId = productID, 
				BookName = productName, 
				Quantity = numberOfProduct,
				Price = productPrice
			};
			_context.OrderDetails.Add(newProduct);

			return Json(true);
		}
	}
}
