using adstra_task.Models;
using adstra_task.Repository;
using adstra_task.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace adstra_task.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(IAccountService accountService, SignInManager<ApplicationUser> signInManager)
        {
            this.accountService = accountService;
            this.signInManager = signInManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
             accountService.Logout();

            return RedirectToAction("index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(Register model)
        {
            var result = await accountService.Register(model);
            
             
            if (result.Succeeded)
            {
                return RedirectToAction("index","Home");
            }

            return View(model);
        }



        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
                
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model,string returnUrl)
        {

            //if (ModelState.IsValid) { }
            var result = await accountService.Login(model);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Profile(string id)
        {
            var user = await accountService.GetCurrentUser(id);

            if(user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}
