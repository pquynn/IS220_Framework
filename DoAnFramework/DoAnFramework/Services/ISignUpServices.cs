using DoAnFramework.Models;
namespace DoAnFramework.Services
{
    interface ISignUpServices
    {
        bool IsUserLoginExists(string userLogin);
        void CreateSignUp(String Username, String UserPhoneNumber, String Userlogin, String Password);
    }
}
