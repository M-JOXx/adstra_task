using adstra_task.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace adstra_task.ViewComponents
{
    public class AdminDetailsViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminDetailsViewComponent(UserManager<ApplicationUser> userManager
            , RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }


        public IViewComponentResult Invoke()
        {
            var data = new AdminDetails()
            {
                Users = userManager.Users.Count(),
                Roles = roleManager.Roles.Count()
            };

            return View(data);
        }

    }
}
