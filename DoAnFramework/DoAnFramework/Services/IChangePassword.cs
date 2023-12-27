using DoAnFramework.Models;

namespace DoAnFramework.Services
{
    interface IChangePassword
    {
        Login GetUserByUserLogin(string userLogin);
        void Update(Login userPass);
    }
}
