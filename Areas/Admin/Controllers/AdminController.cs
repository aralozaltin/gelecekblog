using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BlogProject.Data;
using BlogProject.Models;
using BlogProject.ViewModels;
using System.Linq;

namespace BlogProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public AdminController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Dashboard()
        {
            var model = new AdminDashboardViewModel
            {
                KullaniciSayisi = _userManager.Users.Count(),
                PostSayisi = _context.Posts.Count(p => !p.IsDeleted),
                YorumSayisi = _context.Yorumlar.Count(),
                SonPostlar = _context.Posts
                    .OrderByDescending(p => p.OlusturmaTarihi)
                    .Take(5)
                    .ToList(),
                SonYorumlar = _context.Yorumlar
                    .OrderByDescending(y => y.Tarih)
                    .Take(5)
                    .Include(y => y.AppUser)
                    .Include(y => y.Post)
                    .ToList()
            };

            return View(model);
        }
    }
}
