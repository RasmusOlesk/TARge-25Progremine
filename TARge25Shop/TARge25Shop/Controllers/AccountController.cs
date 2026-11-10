using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TARge25Shop.Models.Account;

namespace TARge25Shop.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _userInManager;

        public AccountController
            (
                UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> _signInManager
            )
        {
            _userManager = userManager;
            _signInManager = SignInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {

                }


                return RedirectToAction("Index")
            }
        }
    }
}
