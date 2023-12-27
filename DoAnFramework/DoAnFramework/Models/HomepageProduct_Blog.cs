using DoAnFramework.Models.ViewModel;

namespace DoAnFramework.Models
{
    public class HomepageProduct_Blog
    {
        public IEnumerable<BookThumbnail> listBook {  get; set; }
        public IEnumerable<Blog> listBlog { get; set; }
        public IEnumerable<BookThumbnail> listBookNew { get; set; }

        public HomepageProduct_Blog(IEnumerable<BookThumbnail> listBook1, IEnumerable<Blog> listBlog1, List<BookThumbnail> listBookNew1)
        {
            this.listBook = listBook1;
            this.listBlog = listBlog1;
            this.listBookNew = listBookNew1;
        }
    }
}
