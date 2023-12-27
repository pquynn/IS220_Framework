using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnFramework.Models.ViewModel
{
    public class SignUpVM
    {

        [Required(ErrorMessage = "Vui lòng nhập họ tên.")]
        public string UserName {  get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        public string UserTelephone { get; set; }
        [Required(ErrorMessage = "Vui lòng điền tên đăng nhập.")]
        public string UserLogin { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        public string UserPassword { get; set; }
        [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu.")]
        [System.ComponentModel.DataAnnotations.Compare("UserPassword")]
        public string ConfirmPassword { get; set; }
        

    }
}
