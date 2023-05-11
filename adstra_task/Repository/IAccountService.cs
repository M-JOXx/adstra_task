using adstra_task.Models;
using Microsoft.AspNetCore.Identity;

namespace adstra_task.Repository
{
    public interface IAccountService
    {
        Task<IdentityResult> Register(Register model);

        Task<SignInResult> Login(LoginViewModel model);
        
        void Logout();

    }
}
