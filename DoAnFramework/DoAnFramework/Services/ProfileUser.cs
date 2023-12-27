using DoAnFramework.Models;
using DoAnFramework.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAnFramework.Services
{
    public class ProfileUser : IProfileUser
    {
        
        public ProfileUser() { }
        //HAi hàm của EditAddress
        public User GetUserByLogin(string userLogin)
        {
            using (var _book_shop_dbContext = new book_shop_dbContext())
            {
                return _book_shop_dbContext.Users.FirstOrDefault(u => u.UserLogin == userLogin);
            }
        }

        public void Update(User userAdd)
        {
            using (var _book_shop_dbContext = new book_shop_dbContext())
            {
                _book_shop_dbContext.Users.Update(userAdd);
                _book_shop_dbContext.SaveChanges();
            }
        }

    }
}
