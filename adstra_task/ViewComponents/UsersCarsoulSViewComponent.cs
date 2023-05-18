using adstra_task.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace adstra_task.ViewComponents
{
    public class UsersCarsoulSViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UsersCarsoulSViewComponent(UserManager<ApplicationUser> userManager
            , RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IViewComponentResult Invoke()
        {
            var data = new UsersCarousel()
            {
                users = userManager.Users.Take(8)
            };

            return View(data);
        }



    }
}
