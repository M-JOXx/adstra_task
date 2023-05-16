using adstra_task.Models;
using adstra_task.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace adstra_task.Repository
{
    public interface IAccountService
    {
        Task<IdentityResult> Register(RegisterViewModel model);

        Task<SignInResult> Login(LoginViewModel model);
        
        Task<ProfileViewModel> GetCurrentUser(string id);

        Task<EditUserViewModel> UpdateUserProfileGet(string Id);
        Task<IdentityResult> UpdateUserProfilePost(EditUserViewModel model);
        void Logout();

    }
}
