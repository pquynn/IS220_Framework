using DoAnFramework.Models;
using DoAnFramework.Models.Service;
using DoAnFramework.Models.ViewModel;
using DoAnFramework.Others;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

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


		//Checkout/Payment (online payment: momo)
		public ActionResult Payment()
		{
            //request params need to request to MoMo system
            string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
			string partnerCode = "MOMOOJOI20210710";
			string accessKey = "iPXneGmrJH0G8FOP";
			string serectkey = "sFcbSGRSJjwGxwhhcEktCHWYUuTuPNDB";
			string orderInfo = "test";
			string returnUrl = "https://localhost:7282/Order/Cart";
			string notifyurl = "https://localhost:7282/Order/Cart"; //lưu ý: notifyurl không được sử dụng localhost, có thể sử dụng ngrok để public localhost trong quá trình test

			string amount = "10000";
			string orderid = DateTime.Now.Ticks.ToString(); //mã đơn hàng
			string requestId = DateTime.Now.Ticks.ToString();
			string extraData = "";

			//Before sign HMAC SHA256 signature
			string rawHash = "partnerCode=" +
				partnerCode + "&accessKey=" +
				accessKey + "&requestId=" +
				requestId + "&amount=" +
				amount + "&orderId=" +
				orderid + "&orderInfo=" +
				orderInfo + "&returnUrl=" +
				returnUrl + "&notifyUrl=" +
				notifyurl + "&extraData=" +
				extraData;

			MoMoSecurity crypto = new MoMoSecurity();
			//sign signature SHA256
			string signature = crypto.signSHA256(rawHash, serectkey);

			//build body json request
			JObject message = new JObject
			{
				{ "partnerCode", partnerCode },
				{ "accessKey", accessKey },
				{ "requestId", requestId },
				{ "amount", amount },
				{ "orderId", orderid },
				{ "orderInfo", orderInfo },
				{ "returnUrl", returnUrl },
				{ "notifyUrl", notifyurl },
				{ "extraData", extraData },
				{ "requestType", "captureMoMoWallet" },
				{ "signature", signature }

			};

			string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

			JObject jmessage = JObject.Parse(responseFromMomo);

			return Redirect(jmessage.GetValue("payUrl").ToString());
		}

		//Khi thanh toán xong ở cổng thanh toán Momo, Momo sẽ trả về một số thông tin, trong đó có errorCode để check thông tin thanh toán
		//errorCode = 0 : thanh toán thành công (Request.QueryString["errorCode"])
		//Tham khảo bảng mã lỗi tại: https://developers.momo.vn/#/docs/aio/?id=b%e1%ba%a3ng-m%c3%a3-l%e1%bb%97i
		public ActionResult BuySuccess(Result result)
		{
			//lấy kết quả Momo trả về và hiển thị thông báo cho người dùng (có thể lấy dữ liệu ở đây cập nhật xuống db)
			string rMessage = result.message;
			string rOrderId = result.orderId;
			string rErrorCode = result.errorCode; // = 0: thanh toán thành công
			return View();
		}

		[HttpPost]
		public void SavePayment()
		{
			//cập nhật dữ liệu vào db
			String a = "";
		}
	}
}


