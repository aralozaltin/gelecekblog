using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BlogProject.Data;
using BlogProject.Models;
using System.Linq;

namespace BlogProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class KategorilerController : Controller
    {
        private readonly AppDbContext _context;

        public KategorilerController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var kategoriler = _context.Kategoriler.ToList();
            return View(kategoriler);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Kategori kategori)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Kategori adı gerekli.";
                return View(kategori);
            }

            _context.Kategoriler.Add(kategori);
            _context.SaveChanges();

            TempData["Success"] = "Kategori başarıyla eklendi!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var kategori = _context.Kategoriler.Find(id);
            if (kategori == null)
                return NotFound();

            return View(kategori);
        }

        [HttpPost]
        public IActionResult Edit(Kategori model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var kategori = _context.Kategoriler.Find(model.Id);
            if (kategori == null)
                return NotFound();

            kategori.Ad = model.Ad;
            _context.SaveChanges();

            TempData["Success"] = "Kategori güncellendi!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var kategori = _context.Kategoriler.Find(id);
            if (kategori == null)
                return NotFound();

            var kategoriKullaniliyorMu = _context.Posts.Any(p => p.KategoriId == id);
            if (kategoriKullaniliyorMu)
            {
                TempData["Error"] = "Bu kategoriye ait gönderiler var. Önce onları taşı veya sil.";
                return RedirectToAction(nameof(Index));
            }

            _context.Kategoriler.Remove(kategori);
            _context.SaveChanges();

            TempData["Success"] = "Kategori başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}
