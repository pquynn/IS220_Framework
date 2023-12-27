using DoAnFramework.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;

namespace DoAnFramework.Models.Service
{
    // OrderService class to handle business logic related to orders
    public class OrderService
    {
        private readonly book_shop_dbContext _context;

        public OrderService(book_shop_dbContext context)
        {
            _context = context;
        }


        //Get user my-order list
        public PagedList<Order> GetMyOrderList(string userId, int page = 1, int pageSize = 20)
        {
            var lsOrders = _context.Orders
                .AsNoTracking()
                .Where(ud => ud.UserId == userId && ud.Status != "cart")
                .OrderByDescending(x => x.OrderId);

            return new PagedList<Order>(lsOrders, page, pageSize);
        }

        //Get my-order detail base on orderid
        public OrderViewModel GetMyOrderDetails(int? orderId)
        {
            if (orderId == null)
            {
                return null;
            }

            var order = _context.Orders
               .AsNoTracking()
                .Where(od => od.OrderId == orderId)
                .Select(od => new OrderViewModel
                {
                    Order = od,
                    OrderDetails = od.OrderDetails.Select(o => new OrderDetailWithImage
                    {
                        OrderDetail = o,
                        FrontCover = o.Book.BookImage.FrontCover
                    })
                })
                .FirstOrDefault();
            return order;
        }

        //Get cart if login by user_id
        public OrderViewModel GetLoginCart(string userId)
        {
            if (userId == null || userId == "")
            {
                return null;
            }

            var cart = _context.Orders
            .AsNoTracking()
            .Where(ud => ud.UserId == userId && ud.Status == "cart")
            .Select(od => new OrderViewModel
            {
                Order = od,
                OrderDetails = od.OrderDetails.Select(o => new OrderDetailWithImage
                {
                    OrderDetail = o,
                    FrontCover = o.Book.BookImage.FrontCover
                })
            })
            .FirstOrDefault();

                return cart;
            }


        //Update order detail quantity
        public bool UpdateOrderDetailQuantity(int orderDetailId, int newQuantity)
        {
            var orderDetail = _context.OrderDetails.Find(orderDetailId);

            if (orderDetail != null)
            {
                orderDetail.Quantity = newQuantity;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        //Delete order detail
        public bool  DeleteOrderDetail(int orderDetailId)
        {
            var orderDetail = _context.OrderDetails.Find(orderDetailId);

            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(orderDetail);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateOrderLogin(string user_id, string name, string phone, 
            string tinhThanh, string quanHuyen, string xaPhuong, string duongAp, DateTime date, string paymentMethod)
        {
            var existingOrder = _context.Orders.SingleOrDefault(o => o.UserId == user_id && o.Status == "cart");

            if (existingOrder != null)
            {
                existingOrder.Address = duongAp + ", " + quanHuyen + ", " + xaPhuong + ", " + tinhThanh;
                existingOrder.OrderDate = date;
                existingOrder.Status = "pending";
                existingOrder.UserName = name;
                existingOrder.UserTelephone = phone;
                existingOrder.Pay = paymentMethod;

                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool InsertOrderLocal(string name, string phone, string tinhThanh, string quanHuyen, 
            string xaPhuong, string duongAp, DateTime date, string paymentMethod, List<LocalCartItem> localCart)
        {
            var newOrder = new Order
            {
                Address = $"{duongAp}, {quanHuyen}, {xaPhuong}, {tinhThanh}",
                OrderDate = date,
                Status = "pending",
                UserName = name,
                UserTelephone = phone,
                Pay = paymentMethod
            };

            _context.Orders.Add(newOrder);
            _context.SaveChanges();

            // Get the order ID of the newly inserted order
            var localOrder = _context.Orders
                .OrderByDescending(od=> od.OrderDate)
                .FirstOrDefault(od => od.UserName == name);
           
            if(localOrder != null)
            {
                var localOrderId = localOrder.OrderId;

                // Insert order details based on localCart
                foreach (var cartItem in localCart)
                {
                    var book = _context.Books.FirstOrDefault(b => b.Name == cartItem.PRODUCT_NAME);
                    if (book == null)
                        return false;
                    
                    var orderDetail = new OrderDetail
                    {
                        OrderId = localOrderId,
                        BookId = book.BookId,
                        BookName = cartItem.PRODUCT_NAME,
                        Quantity = cartItem.QUANTITY,
                        Price = cartItem.PRICE
                    };

                    _context.OrderDetails.Add(orderDetail);
                    
                }
                _context.SaveChanges();
                return true;
            }
             return false;
        }

        //cancel order 
        public bool updateOrderStatus(int orderId, string status)
        {
            try
            {
                var order = _context.Orders.FirstOrDefault(o => o.OrderId == orderId);

                if (order != null)
                {
                    order.Status = status;
                    _context.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                // Handle exception or log it
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //find order
        public int findLatestMoMoOrder()
        {
            var order = _context.Orders
                .Where(od => od.Pay == "momo-wallet" && od.Status == "pending")
                .OrderByDescending(od => od.OrderDate)
                .FirstOrDefault();
            if (order != null)
                return order.OrderId;

            else return -1;
        }
    }
}
