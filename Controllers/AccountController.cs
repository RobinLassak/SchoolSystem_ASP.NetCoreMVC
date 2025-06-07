using ASP.NetCoreMVC_SchoolSystem.Models;
using ASP.NetCoreMVC_SchoolSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NetCoreMVC_SchoolSystem.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        UserManager<AppUsers> _userManager;
        SignInManager<AppUsers> _signInManager;
        public AccountController(UserManager<AppUsers> userManager, SignInManager<AppUsers> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            LoginViewModel model = new LoginViewModel();
            model.ReturnURL = returnUrl;
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //Prihlaseni uzivatele
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                AppUsers userToLogin = await _userManager.FindByNameAsync(login.UserName);
                if (userToLogin != null)
                {
                    var signInResult = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, login.Remember, false);
                    if (signInResult.Succeeded)
                    {
                        return Redirect(login.ReturnURL ?? "/");
                    }
                }
            }
            ModelState.AddModelError("", "User not found or wrong password");
            return View(login);
        }
        //Odhlaseni uzivatele
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
