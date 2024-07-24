using IdentityApp.Models;
using IdentityApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityApp.Controllers
{
    // Rol atamali yetki seviyesi
    [Authorize(Roles = "admin")]
    // [AllowAnonymous] -> Herkes erisebilir yetkiye ihtiyac yoktur
    public class UsersController : Controller
    {
        // User bilgileri + Rol Bilgileri = UserManager //
        private UserManager<AppUser> _userManager;

        // Veritabani rol bilgilerini al
        private RoleManager<AppRole> _roleManager;

        public UsersController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            // _userManager.Users -> Butun user bilgilerini getir //
            return View(_userManager.Users);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                // Sadece rol ismini almak
                ViewBag.Roles = await _roleManager.Roles.Select(i => i.Name).ToListAsync();

                return View(new EditViewModel
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    // Daha onceden secilen rolleri de listeleme
                    SelectedRoles = await _userManager.GetRolesAsync(user)
                });
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, EditViewModel model)
        {
            if (id != model.Id)
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);

                if (user != null)
                {
                    user.Email = model.Email;
                    user.FullName = model.FullName;

                    var result = await _userManager.UpdateAsync(user);

                    // Parola varsa parola guncellemesi yap
                    if (result.Succeeded && !string.IsNullOrEmpty(model.Password))
                    {
                        // Parola kaldir
                        await _userManager.RemovePasswordAsync(user);
                        // Modelden gelen parolayi ekle
                        await _userManager.AddPasswordAsync(user, model.Password);
                    }

                    if (result.Succeeded)
                    {
                        // Rolleri sil, secilen rolleri yeniden ekle
                        await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
                        if (model.SelectedRoles != null)
                        {
                            // Rolleri ekle
                            await _userManager.AddToRolesAsync(user, model.SelectedRoles);
                        }
                        return RedirectToAction("Index");
                    }

                    // Varsa hata mesajlarini yazdir
                    foreach (IdentityError err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction("Index");
        }
    }
}