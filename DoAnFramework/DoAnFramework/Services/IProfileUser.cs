using DoAnFramework.Models;
using DoAnFramework.Models.ViewModel;

namespace DoAnFramework.Services
{
    interface IProfileUser
    {
       // ProfileUserVM GetCurrentUser();
       // void SaveEditProfile(ProfileUserVM user);
        User GetUserByLogin(string userLogin);
        void Update(User userAdd);
    }
}
