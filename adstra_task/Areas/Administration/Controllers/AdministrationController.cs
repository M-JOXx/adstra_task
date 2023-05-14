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
    [Authorize(Roles = "admin,user")]
    public class AdministrationController : Controller
    {
        private readonly IAdminService adminService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper _mapper;

        public AdministrationController(IAdminService adminService
            ,RoleManager<IdentityRole> roleManager
            ,IMapper mapper)
        {
            this.adminService = adminService;
            this.roleManager = roleManager;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var users =  adminService.AllUsers();

            ViewBag.Roles = roleManager.Roles.Count();
            ViewBag.Users = users.Count();

            return View(users);
        }
        

        [HttpGet]
        public IActionResult CreateRole() => View();



        [HttpPost]
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
        public async Task<IActionResult> ManageRoles(string id)
        {
            return View(await adminService.ManageRolesGet(id));
        }   
        

        [HttpPost]
        public async Task<IActionResult> ManageRoles(List<UserRoleManager> model,string id)
        {
            var result = await adminService.ManageRolesPost(model,id);

            if (result == null)
            {
                ViewBag.ErrorMessage = $"Role Name with This Id {id} Cannot Be Found!";
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
