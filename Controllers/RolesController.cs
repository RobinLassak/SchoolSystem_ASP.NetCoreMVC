using ASP.NetCoreMVC_SchoolSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NetCoreMVC_SchoolSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<AppUsers> _userManager;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<AppUsers> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View(_roleManager.Roles.OrderBy(role => role.Name));
        }
        //Vytvareni nove role
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddIdentityErrors(result);
                }
            }
            return View(name);
        }
        //Mazani role
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole roleToDelete = await _roleManager.FindByIdAsync(id);
            if (roleToDelete != null)
            {
                var result = await _roleManager.DeleteAsync(roleToDelete);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddIdentityErrors(result);
                }
            }
            ModelState.AddModelError("", "Role not found");
            return View("Index", _roleManager.Roles);
        }
        //Editace role
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            IdentityRole roleToEdit = await _roleManager.FindByIdAsync(id);
            List<AppUsers> members = new List<AppUsers>();
            List<AppUsers> nonMembers = new List<AppUsers>();
            foreach(var user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, roleToEdit.Name) ? members : nonMembers;
                list.Add(user);
            }
            return View(new RoleState
            {
                Members = members,
                NonMembers = nonMembers,
                Role = roleToEdit,
            });
        }
        public async Task<IActionResult> EditAsync(RoleModification roleModification)
        {
            if (ModelState.IsValid)
            {
                foreach (string userId in roleModification.AddIds ?? new string[] {})
                {
                    AppUsers userToAdd = await _userManager.FindByIdAsync(userId);
                    if(userToAdd != null)
                    {
                        IdentityResult result = await _userManager.AddToRoleAsync(userToAdd, roleModification.RoleName);
                        if (!result.Succeeded)
                        {
                            AddIdentityErrors(result);
                        }
                    }
                }
                foreach(string userId in roleModification.DeleteIds ?? new string[] { })
                {
                    AppUsers userToDelete = await _userManager.FindByIdAsync(userId);
                    if(userToDelete != null)
                    {
                        IdentityResult result = await _userManager.RemoveFromRoleAsync(userToDelete, roleModification.RoleName);
                        if (!result.Succeeded)
                        {
                            AddIdentityErrors(result);
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Spatne zadana zmena, zkontroluj udaje");
            return RedirectToAction("Index");
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
