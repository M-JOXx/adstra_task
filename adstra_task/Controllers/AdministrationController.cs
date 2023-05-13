using adstra_task.Models;
using adstra_task.Repository;
using adstra_task.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace adstra_task.Controllers
{
    [Authorize(Roles = "admin,user")]
    public class AdministrationController : Controller
    {
        private readonly IAdminService adminService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper _mapper;

        public AdministrationController(IAdminService adminService,RoleManager<IdentityRole> roleManager
            ,UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            this.adminService = adminService;
            this.roleManager = roleManager;
            this.userManager = userManager;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("Dashboard")]
        public async Task<IActionResult> Index()
        {
            var users = adminService.AllUsers();
            
            return View(users);
        }
        

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
         
                var result = adminService.CreateRole(model);

                if (result.Result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var errors in result.Result.Errors)
                {
                    ModelState.AddModelError("", errors.Description);
                }

            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> ManageRoles(string id)
        {
            var GetRoles = await adminService.ManageRolesGet(id);
            return View(GetRoles);
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
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }


            return RedirectToAction("Index");
        }

        //public async Task<string> InUse()
        //{

        //    return "a";
        //}
    }
}
