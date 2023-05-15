using adstra_task.Models;
using adstra_task.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace adstra_task.Repository
{
    public interface IAccountService
    {
        Task<IdentityResult> Register(Register model);

        Task<SignInResult> Login(LoginViewModel model);
        
        Task<ProfileViewModel> GetCurrentUser(string id);

        Task<EditUserViewModel> UpdateUserProfileGet(string id);
        Task<IdentityResult> UpdateUserProfilePost(EditUserViewModel model);
        void Logout();

    }
}
