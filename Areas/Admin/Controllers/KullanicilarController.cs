using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BlogProject.Models;


namespace BlogProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class KullanicilarController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public KullanicilarController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.Where(u => !u.IsDeleted).ToList();

            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> AdminYap(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null && !await _userManager.IsInRoleAsync(user, "Admin"))
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AdminliktenAl(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null && await _userManager.IsInRoleAsync(user, "Admin"))
            {
                await _userManager.RemoveFromRoleAsync(user, "Admin");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Sil(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            user.IsDeleted = true;
            await _userManager.UpdateAsync(user);

            TempData["Success"] = "Kullanıcı silindi (devre dışı bırakıldı).";
            return RedirectToAction("Index");
        }

    }
}
