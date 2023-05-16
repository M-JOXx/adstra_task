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

        public AccountService(UserManager<ApplicationUser> userManager
            ,SignInManager<ApplicationUser> signInManager
            ,RoleManager<IdentityRole> roleManager,IMapper mapper)
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


        public async Task<IdentityResult> Register(RegisterViewModel model)
        {
            var user = mapper.Map<ApplicationUser>(model);

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user,isPersistent: false);
                return result;
            }
            
            return result;
        }


        public async Task<SignInResult> Login(LoginViewModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password
                , model.RememberMe, false);
            
            return result;
        }
        
        public async Task<ProfileViewModel> GetCurrentUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            return mapper.Map<ProfileViewModel>(user);
        }

        public async Task<EditUserViewModel> UpdateUserProfileGet(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);

            if (user == null) return null;

           
            return mapper.Map<EditUserViewModel>(user);

        }

        public async Task<IdentityResult> UpdateUserProfilePost(EditUserViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);

            if (user == null ) return null;
            else
            {
                mapper.Map<EditUserViewModel, ApplicationUser>(model, user);

                var result = await userManager.UpdateAsync(user);

                return result;
            }
        }

    }
}
