namespace DoAnFramework.Models
{
    public class HomepageProduct_Blog
    {
        public List<Book> listBook {  get; set; }
        public List<Blog> listBlog { get; set; }
        public List<Book> listBookNew { get; set; }

        public HomepageProduct_Blog(List<Book> listBook1, List<Blog> listBlog1, List<Book> listBookNew1)
        {
            this.listBook = listBook1;
            this.listBlog = listBlog1;
            this.listBookNew = listBookNew1;
        }
    }
}
