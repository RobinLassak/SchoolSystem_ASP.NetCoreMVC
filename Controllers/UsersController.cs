using ASP.NetCoreMVC_SchoolSystem.Models;
using ASP.NetCoreMVC_SchoolSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NetCoreMVC_SchoolSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        UserManager<AppUsers> _userManager;
        IPasswordHasher<AppUsers> _passwordHasher;
        IPasswordValidator<AppUsers> _passwordValidator;
        public UsersController(UserManager<AppUsers> userManager, IPasswordHasher<AppUsers> passwordHasher, 
            IPasswordValidator<AppUsers> passwordValidator)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _passwordValidator = passwordValidator;
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
        //Uprava uzivatele
        public async Task<IActionResult> EditAsync(string id)
        {
            var userToEdit = await _userManager.FindByIdAsync(id);
            if (userToEdit == null)
            {
                return View("NotFound");
            }
            return View(userToEdit);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(string id, string username, string email, string password)
        {
            AppUsers userToEdit = await _userManager.FindByIdAsync(id);
            if (userToEdit == null)
            {
                return View("NotFound");
            }
            if (!string.IsNullOrEmpty(username))
            {
                userToEdit.UserName = username;
            }
            if (!string.IsNullOrEmpty(email))
            {
                userToEdit.Email = email;
            }
            else
            {
                ModelState.AddModelError("", "E-mail cannot be empty");
            }
            IdentityResult validPass = null;
            if (!string.IsNullOrEmpty(password))
            {
                validPass = await _passwordValidator.ValidateAsync(_userManager, userToEdit, password);
                if (validPass.Succeeded)
                {
                    userToEdit.PasswordHash = _passwordHasher.HashPassword(userToEdit, password);
                }
                else
                {
                    AddIdentityErrors(validPass);
                }
                
            }
            else
            {
                ModelState.AddModelError("", "Password cannot be empty");
            }
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                if (validPass.Succeeded)
                {
                    IdentityResult result = await _userManager.UpdateAsync(userToEdit);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                        AddIdentityErrors(result);
                }
            }
            return View(userToEdit);
        }

        //Smazani uzivatele
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            AppUsers userToDelete = await _userManager.FindByIdAsync(id);
            if (userToDelete != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(userToDelete);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddIdentityErrors(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "User not found");
                return View("Index", _userManager.Users);
            }
            return View(userToDelete);
        }
        //Pomocne metody
        private void AddIdentityErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}
