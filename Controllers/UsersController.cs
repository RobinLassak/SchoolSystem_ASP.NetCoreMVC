using ASP.NetCoreMVC_SchoolSystem.Models;
using ASP.NetCoreMVC_SchoolSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NetCoreMVC_SchoolSystem.Controllers
{
    public class UsersController : Controller
    {
        UserManager<AppUsers> _userManager;
        public UsersController(UserManager<AppUsers> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View(_userManager.Users);
        }
        //Vytvareni noveho uzivatele
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(UserViewModel newUser)
        {
            if (ModelState.IsValid)
            {
                AppUsers userToAdd = new AppUsers()
                {
                    UserName = newUser.Name,
                    Email = newUser.Email,
                };
                IdentityResult result = await _userManager.CreateAsync(userToAdd, newUser.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(newUser);
        }
    }
}
