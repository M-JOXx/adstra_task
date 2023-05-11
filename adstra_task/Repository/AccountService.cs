using adstra_task.Models;
using Microsoft.AspNetCore.Identity;

namespace adstra_task.Repository
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {

            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IdentityResult> Register(Register model)
        {
            var user = new ApplicationUser {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                City = model.City,
            };

            return await userManager.CreateAsync(user,model.Password);
        }

        public async Task<SignInResult> Login(LoginViewModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            return result;
        }

        public  void Logout()
        {
              signInManager.SignOutAsync();
        }

       
    }
}
