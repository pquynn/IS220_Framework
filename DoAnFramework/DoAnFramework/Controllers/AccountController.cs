using System.Security.Cryptography;
using System.Text;
using DoAnFramework.Models;
using DoAnFramework.Models.ViewModel;
using DoAnFramework.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace DoAnFramework.Controllers
{
    public class AccountController : Controller
    {
        private readonly book_shop_dbContext _book_shop_dbContext;
        ISignUpServices signUpServices;
        IProfileUser profile;
        IChangePassword changePass;
        public AccountController(book_shop_dbContext book_shop)
        {
            _book_shop_dbContext = book_shop;
        }
        public IActionResult Index() //account profile
        {

            var user_id = HttpContext.Session.GetString("userId");
            if (user_id == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        
        // Chỉnh sửa thông tin
        public  IActionResult EditProfileUser()
        {
            ProfileUserVM currentUser = GetCurrentUser();
            if (currentUser != null)
            {
                return View(currentUser);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult EditProfileUser(ProfileUserVM profileVM)
        {
            profile = new ProfileUser();
            if (ModelState.IsValid)
            {
                var user = profile.GetUserByLogin(HttpContext.Session.GetString("userLogin"));

                if (user != null)
                {
                    // Cập nhật thông tin người dùng từ AddressViewModel
                    user.UserName = profileVM.UserName;
                    user.Birthday = profileVM.Birthday;
                    user.Gender = profileVM.Gender;

                    // Lưu thay đổi vào cơ sở dữ liệu
                    profile.Update(user);
                    return RedirectToAction("Index");                  
                }
            }          
            return View(profileVM);
        }
        
        // Thay đổi mật khẩu
        public IActionResult ChangePassword()
        {
            var model = new ChangePasswordVM();
            return View(model);
        }
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordVM model)
        {
            changePass = new ChangePassword();
            if (ModelState.IsValid)
            {
                var user = changePass.GetUserByUserLogin(HttpContext.Session.GetString("userLogin"));
                if (user != null)
                {
                    user.UserPassword = model.NewPassword; // Hash mật khẩu mới trước khi lưu vào cơ sở dữ liệu
                    changePass.Update(user); // Cập nhật thông tin người dùng trong cơ sở dữ liệu

                    return RedirectToAction("Login");
                }
            }
            return View(model);
        }

        //Chỉnh sửa địa chỉ
        public IActionResult EditAddress()
        {
            EditAddressVM currentUser1 = GetCurrentUser1();
            if (currentUser1 != null)
            {
                return View(currentUser1);
            }
            else
            {
                return NotFound();
            }
            return View();
        }

        
        [HttpPost]

        public IActionResult EditAddress(EditAddressVM editAdd)
        {
            profile = new ProfileUser();
            if (ModelState.IsValid)
            {
                var user = profile.GetUserByLogin(HttpContext.Session.GetString("userLogin"));

                if (user != null)
                {
                    // Cập nhật thông tin người dùng từ AddressViewModel
                    user.UserName = editAdd.UserName;
                    user.UserTelephone = editAdd.PhoneNumber;
                    user.Address = editAdd.Address;

                    // Lưu thay đổi vào cơ sở dữ liệu
                    profile.Update(user);

                    return RedirectToAction("Index");
                }
            }
            return View(editAdd);
        }

        // Login
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(String username, String password)
        {
            if (string.IsNullOrEmpty(username) == true | string.IsNullOrEmpty(password) == true)
            {
                ViewBag.error = "User và password chưa nhập.";
                return View();
            }

            var m_password = GetMD5(password);
            var user = _book_shop_dbContext.Logins.SingleOrDefault(m => m.UserLogin.ToLower() == username.ToLower());

            if (user == null)
            {
                ViewBag.error = "Tài khoản không tồn tại";
                return View();
            }
            else
            {
                var tbl_user = _book_shop_dbContext.Users
                      .Where(u => u.UserLogin == user.UserLogin)
                      .FirstOrDefault();

                HttpContext.Session.SetString("userId", tbl_user.UserId);
                HttpContext.Session.SetString("userLogin", user.UserLogin);
                HttpContext.Session.SetString("roleId", user.RoleId.ToString());
            }

            if (user.UserPassword != password)
            {
                ViewBag.error = "Mật khẩu không đúng!";
                return View();
            }

            return RedirectToAction("Index");

        }

        // SignUp
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(SignUpVM viewModel)
        {
            signUpServices = new SignUpServices();
            if (ModelState.IsValid)
            {
                if (signUpServices.IsUserLoginExists(viewModel.UserLogin))
                {
                    viewModel.UserLogin = "";
                    ViewBag.error += "Tên đăng nhập đã tồn tại!";
                    return View(viewModel);
                }
                else 
                {
                    signUpServices.CreateSignUp(viewModel.UserName, viewModel.UserTelephone, viewModel.UserLogin, (viewModel.UserPassword));
                    return RedirectToAction("Login");
                }
               
            }
                return View();
        }

        // Forgetpassword
        public IActionResult ForgetPassword()
        {
            
            return View(); 
        }

        public IActionResult ForgetPassword2()
        {
            return View();
        }


        // LogOut
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        //Hàm mã hóa mật khẩu
        public string GetMD5(string str)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(str);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        // Lấy dữ liệu lên view cho đổi địa chỉ
        private EditAddressVM GetCurrentUser1()
        {
            using (var _book_shop_dbContext = new book_shop_dbContext())
            {
                string userLogin = HttpContext.Session.GetString("userLogin");
                var user = _book_shop_dbContext.Users.FirstOrDefault(u => u.UserLogin == userLogin);
                if (user != null)
                {
                    return new EditAddressVM
                    {
                        UserName = user.UserName,
                        PhoneNumber = user.UserTelephone,
                        Address = user.Address
                    };
                }
            }
            return null;
        }

        // Lấy dữ liệu lên view cho chỉnh sửa thông tin
        private ProfileUserVM GetCurrentUser()
        {
            using (var _book_shop_dbContext = new book_shop_dbContext())
            {
                string userLogin = HttpContext.Session.GetString("userLogin");
                var user = _book_shop_dbContext.Users.FirstOrDefault(u => u.UserLogin == userLogin);
                if (user != null)
                {
                    return new ProfileUserVM
                    {
                        UserName = user.UserName,
                        Birthday = user.Birthday,
                        Gender = user.Gender
                    };
                }
            }
            return null;
        }


        [HttpPost]
        public IActionResult getSession(string sessionName)
        {
            return Json(HttpContext.Session.GetString(sessionName));
        }

    }
}
