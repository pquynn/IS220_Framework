using DoAnFramework.Models.ViewModel;

namespace DoAnFramework.Models.Service
{
    public class CommentService
    {
        private readonly book_shop_dbContext _context;

        public CommentService(book_shop_dbContext context)
        {
            _context = context;
        }

        //Get my-order detail base on orderid
        public OrderWithComment GetMyOrderWithComment(int? orderId)
        {
            if (orderId == null)
            {
                return null;
            }
            //lay don hang
            var order = _context.Orders
                .Where(od => od.OrderId == orderId)
                .Select(od => new OrderViewModel
                {
                    Order = od,
                    OrderDetails = od.OrderDetails.Select(o => new OrderDetailWithImage
                    {
                        OrderDetail = o,
                        FrontCover = o.Book.BookImage.FrontCover,
                    })
                })
                .FirstOrDefault();

            //lay ds comment
            List<Comment> comments = new List<Comment>();
            foreach(var detail in order.OrderDetails)
            {
                var comment = _context.Comments
                    .Where(cmt => cmt.CmtId == detail.OrderDetail.OrderDetailId)
                    .FirstOrDefault();
                if(comment != null)
                {
                    comments.Add(comment);
                }
            }

            //luu vao model view
            var owc = new OrderWithComment();
            owc.Order = order;
            owc.Comment = comments;

            return owc;
        }

        public bool addFeedback(string userId, string userName, List<CommentList> comments)
        {
            foreach (var comment in comments)
            {
                // Find the book_id based on book_name
                var existingComment = _context.Comments.FirstOrDefault(c => c.CmtId == comment.comtId);

                if (existingComment != null)
                {
                    existingComment.CmtDay = DateTime.Now;
                    existingComment.Score = comment.score;
                    existingComment.Content = comment.content;
                }
                else
                {
                    var newcmt = new Comment
                    {
                        CmtId = comment.comtId,
                        UserName = userName,
                        UserId = userId,
                        CmtDay = DateTime.Now,
                        Score = comment.score,
                        Content = comment.content,
                        BookId = comment.bookId,
                        BookName = comment.bookName
                    };
                    _context.Comments.Add(newcmt);
                }
            }
            _context.SaveChanges();
            return true;
        }
    }
}
