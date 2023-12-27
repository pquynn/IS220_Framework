using DoAnFramework.Models;

namespace DoAnFramework.Services
{
    public class ChangePassword: IChangePassword
    {
        public ChangePassword() { }

        //Hàm lấy dữ liệu từ CSDL lên web
        public Login GetUserByUserLogin(string userLogin)
        {
            using (var _book_shop_dbContext = new book_shop_dbContext())
            {
                return _book_shop_dbContext.Logins.FirstOrDefault(u => u.UserLogin == userLogin);
            }
        }
        public void Update(Login userPass)
        {
            using (var _book_shop_dbContext = new book_shop_dbContext())
            {
                _book_shop_dbContext.Logins.Update(userPass);
                _book_shop_dbContext.SaveChanges();
            }
        }

    }
}
