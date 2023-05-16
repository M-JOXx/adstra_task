using adstra_task.Models;
using adstra_task.Repository;
using adstra_task.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace adstra_task.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Route("[Controller]/[Action]")]
    public class AdministrationController : Controller
    {
        private readonly IAdminService adminService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper _mapper;

        public AdministrationController(IAdminService adminService
            ,UserManager<ApplicationUser> userManager
            ,RoleManager<IdentityRole> roleManager
            ,IMapper mapper)
        {
            this.adminService = adminService;
            this.userManager = userManager;
            this.roleManager = roleManager;
            _mapper = mapper;
        }


        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public IActionResult Index() => View(adminService.AllUsers());


        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult CreateRole() => View();



        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid )
            {
                    var result = await adminService.CreateRole(model);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    foreach (var errors in result.Errors)
                    {
                        ModelState.AddModelError("", errors.Description);
                    }
            }
       
            return View(model);
        }


        [HttpGet]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> ManageRoles(string Id,string userId)
        {
            var user = await userManager.FindByIdAsync(Id);
            var user2 = await userManager.FindByIdAsync(userId);

            var userR1 = await userManager.IsInRoleAsync(user, "admin");
            var userR2 = await userManager.IsInRoleAsync(user2, "admin");

            if (!userR1 || !userR2)
            {
                var result = await adminService.ManageRolesGet(Id);
                return View(result);
            }

            return RedirectToAction("Index");    
        }   
        

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ManageRoles(List<UserRoleManager> model,string Id)
        {
            var result = await adminService.ManageRolesPost(model,Id);

            if (result == null)
            {
                ViewBag.ErrorMessage = $"Role Name with This Id {Id} Cannot Be Found!";
                return View("ErrorPage");
            }
            

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }


            return RedirectToAction("Index");
        }


    }
}
