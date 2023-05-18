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
        public IActionResult Users(string? EAccess,string? Error)
        {
            var data = adminService.AllUsers();
            if (EAccess != null || Error != null)
            {
                ViewBag.ErrorAccess = EAccess;
                ViewBag.Error = Error;
            }

            return View(data);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Roles()
        {
            var data = adminService.AllRoles();
           

            return View(data);
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult CreateRole() => View();



        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
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


        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
           
            var roleResult = await userManager.IsInRoleAsync(user, "admin");

            if (roleResult == false)
            {
                var result = await adminService.DeleteUserPost(user.Id);
                if (result.Succeeded)
                {
                    return RedirectToAction("Users", "Administration");
                }
            }

                var error = $"User with this Id = [{id}] can't be deleted!";

                return RedirectToAction("Users", "Administration", new { Error = error });

        }


        [HttpGet]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> ManageRoles(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);


            var userR = await userManager.IsInRoleAsync(user, "admin");
        

            if (!userR)
            {
                var result = await adminService.ManageRolesGet(Id);
                return View(result);
            }

            var error = "You don't have a permission ,Admin";

            return RedirectToAction("Users", "Administration",new { EAccess = error });    
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


            return RedirectToAction("Users");
        }


    }
}
