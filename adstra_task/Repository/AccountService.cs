using adstra_task.Models;
using adstra_task.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace adstra_task.Repository
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;

        public AccountService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,RoleManager<IdentityRole> roleManager,IMapper mapper)
        {

            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
        }

        public void Logout()
        {
            signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> Register(Register model)
        {

            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                City = model.City,
            };

            await signInManager.SignInAsync(user,isPersistent: false);

            return await userManager.CreateAsync(user,model.Password);
        }

        public async Task<SignInResult> Login(LoginViewModel model)
        {
            
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            return result;
        }
        
        public async Task<ProfileViewModel> GetCurrentUser(string id)
        {

            var user = await userManager.FindByIdAsync(id);
            

            return mapper.Map<ProfileViewModel>(user);
        }
    }
}
