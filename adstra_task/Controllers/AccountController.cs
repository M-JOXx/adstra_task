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

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Logout()
        {
            accountService.Logout();
            return RedirectToAction("index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register() => View();


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                var result = await accountService.Register(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Email", "This Email is already Taken");
                    return View(model);
                }
            }

            return View(model);
        }



        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login() => View();
                
        

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model,string? returnUrl )
        {
            if (ModelState.IsValid)
            {
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
                else
                {
                    ModelState.AddModelError(string.Empty, "Something wrong in Email or Password!");
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

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> EditUserProfile(string Id)
        {
            return View(await accountService.UpdateUserProfileGet(Id));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EditUserProfile(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await accountService.UpdateUserProfilePost(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("Profile", new { id = model.Id });
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View(model);
                }
            }

            return View(model);
        }



    }
}
