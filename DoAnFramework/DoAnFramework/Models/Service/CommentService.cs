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
                    _context.SaveChanges();
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
                    _context.SaveChanges();
                }
            }
            //_context.SaveChanges();
            return true;
        }
    }
}
