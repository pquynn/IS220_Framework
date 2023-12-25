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

        public PagedList<Order> GetOrdersById(string userId, int page = 1, int pageSize = 20)
        {
            var lsOrders = _context.Orders
                .AsNoTracking()
                .Where(ud => ud.UserId == userId && ud.Status != "cart")
                .OrderByDescending(x => x.OrderId);

            return new PagedList<Order>(lsOrders, page, pageSize);
        }

        public Order GetOrderDetails(int? orderId)
        {
            if (orderId == null)
            {
                return null;
            }

            var order = _context.Orders
                .AsNoTracking()
                .Where(od => od.OrderId == orderId)
                .Include(x => x.OrderDetails)
                    .ThenInclude(od => od.Book) // Include the related Book for each OrderDetail
                        .ThenInclude(od => od.BookImage)
                .FirstOrDefault();

            return order;
        }

        public Order GetLoginCart(string userId)
        {
            if (userId == null || userId == "")
            {
                return null;
            }

            var cart = _context.Orders
                .AsNoTracking()
                .Include(od => od.OrderDetails)
                    .ThenInclude(od => od.Book.BookImage) // Include the related Book and BookImage for each OrderDetail
                .FirstOrDefault(ud => ud.UserId == userId && ud.Status == "cart");

            return cart;
        }
    }

}
