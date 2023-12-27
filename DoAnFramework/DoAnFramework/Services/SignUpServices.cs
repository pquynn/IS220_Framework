using DoAnFramework.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace DoAnFramework.Services
{
    public class SignUpServices: ISignUpServices
    {
       
        public SignUpServices() { }
        
        public bool IsUserLoginExists(string userLogin)
        {
            book_shop_dbContext _book_shop_dbContext = new book_shop_dbContext();
            Login checkUserLogin = (from Logins in _book_shop_dbContext.Logins
                                    where Logins.UserLogin.Equals(userLogin)
                                    select Logins).SingleOrDefault();
            if (checkUserLogin != null)
            {
                return true;
            }
            return false;
        }

        public string GenerateUserId()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var userId = new string(Enumerable.Repeat(chars, 5)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            return userId;
        }

        public void CreateSignUp(String Username, String UserPhoneNumber, String Userlogin, String Password)
        {
            using (var _book_shop_dbContext = new book_shop_dbContext())
            {
                 var newUser = new User
                 {
                    UserId = GenerateUserId(),
                    UserLogin = Userlogin,
                    UserName = Username,
                    UserTelephone = UserPhoneNumber,
                    DayAdd = DateTime.Now

            };
                var newLogin = new Login
                {
                    UserLogin = Userlogin,
                    UserPassword = Password,
                    RoleId = 3
                };

                _book_shop_dbContext.Add(newUser);
                _book_shop_dbContext.Add(newLogin);
                _book_shop_dbContext.SaveChanges();
             }
        }
            
    }
}
