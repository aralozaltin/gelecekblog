using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BlogProject.Models;
using BlogProject.ViewModels;
using BlogProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace BlogProject.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IKategoriRepository _kategoriRepository;
        private readonly IYorumRepository _yorumRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _env;

        public PostsController(
            IPostRepository postRepository,
            IKategoriRepository kategoriRepository,
            IYorumRepository yorumRepo,
            UserManager<AppUser> userManager,
            IWebHostEnvironment env)
        {
            _postRepository = postRepository;
            _kategoriRepository = kategoriRepository;
            _yorumRepo = yorumRepo;
            _userManager = userManager;
            _env = env;
        }

        // GET: /Posts
        [AllowAnonymous]
        public async Task<IActionResult> Index(int? kategoriId)
        {
            ViewBag.Kategoriler = await _kategoriRepository.GetAllAsync();
            ViewBag.SelectedKategoriId = kategoriId;

            var posts = kategoriId.HasValue
                ? await _postRepository.FindAsync(p => !p.IsDeleted && p.KategoriId == kategoriId.Value)
                : await _postRepository.FindAsync(p => !p.IsDeleted);

            return View(posts.OrderByDescending(p => p.OlusturmaTarihi).ToList());
        }

        // GET: /Posts/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Kategoriler = await _kategoriRepository.GetAllAsync();
            return View();
        }

        // POST: /Posts/Create
        [HttpPost]
        public async Task<IActionResult> Create(PostCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Kategoriler = await _kategoriRepository.GetAllAsync();
                return View(model);
            }

            string gorselYolu = "";
            if (model.Gorsel != null && model.Gorsel.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid() + Path.GetExtension(model.Gorsel.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Gorsel.CopyToAsync(stream);
                }

                gorselYolu = "/uploads/" + uniqueFileName;
            }

            var user = await _userManager.GetUserAsync(User);

            var post = new Post
            {
                Baslik = model.Baslik,
                Icerik = model.Icerik,
                KategoriId = model.KategoriId,
                GorselYolu = gorselYolu,
                OlusturmaTarihi = DateTime.Now,
                AppUserId = user.Id
            };

            await _postRepository.AddAsync(post);
            return RedirectToAction("Index");
        }

        // GET: /Posts/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var post = await _postRepository.GetPostWithFullDetailsAsync(id);
            if (post == null)
                return NotFound();

            return View(post);
        }

        // POST: /Posts/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null) return NotFound();

            var currentUser = await _userManager.GetUserAsync(User);

            if (post.AppUserId != currentUser.Id && !User.IsInRole("Admin"))
                return Forbid();

            post.IsDeleted = true;
            await _postRepository.UpdateAsync(post);

            TempData["Success"] = "Gönderi başarıyla silindi.";
            return RedirectToAction("Index");
        }

        // POST: /Posts/YorumEkle
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> YorumEkle(YorumEkleViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Details", new { id = model.PostId });

            var user = await _userManager.GetUserAsync(User);

            var yorum = new Yorum
            {
                Metin = model.Metin,
                PostId = model.PostId,
                AppUserId = user.Id,
                Tarih = DateTime.Now
            };

            await _yorumRepo.AddAsync(yorum);
            return RedirectToAction("Details", new { id = model.PostId });
        }
    }
}
