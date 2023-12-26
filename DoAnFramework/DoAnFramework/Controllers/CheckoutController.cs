using DoAnFramework.Models;
using DoAnFramework.Models.Service;
using DoAnFramework.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAnFramework.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly OrderService _orderService;

        public CheckoutController(OrderService orderService)
        {
            _orderService = orderService;
        }

        //GET: Checkout/Index/"KH009"
        public IActionResult Index(string user_id = "")
        {
            var cartViewModel = _orderService.GetLoginCart(user_id);

            if (cartViewModel == null)
            {
                return View(); // Handle the case where the cart is null
            }

            return View(cartViewModel);
        }

        //GET: Checkout/BuySuccess
        public IActionResult BuySuccess()
        {
            return View();
        }

        //POST: Checkout/Buy/...
        [HttpPost]
        public IActionResult Buy(string user_id, string name, string phone,
               string tinhThanh, string quanHuyen, string xaPhuong, string duongAp,
              DateTime date, string paymentMethod, List<LocalCartItem> localCart)
        {
            try
            {
                if (!string.IsNullOrEmpty(user_id))
                {
                    // Update existing order
                    if(_orderService.UpdateOrderLogin(user_id, name, phone, tinhThanh, quanHuyen, xaPhuong, duongAp, date, paymentMethod))
                        return Json(new { success = true, message = "Mua thành công"});
                    else return Json(new { success = false, message = "Mua không thành công" });
                }
                else
                {
                    // Insert new order
                    if (_orderService.InsertOrderLocal(name, phone, tinhThanh, quanHuyen, xaPhuong, duongAp, date, paymentMethod, localCart))
                        return Json(new { success = true, message = "Mua thành công", localcart = localCart });
                    else return Json(new { success = false, message = "Mua không thành công" });

                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine(ex.Message);
                return Json(new { success = false, error = ex.Message });
            }
        }


    }
}


