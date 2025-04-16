using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BlogProject.Data;
using Microsoft.AspNetCore.Identity;
using BlogProject.Models;

[Authorize]
public class YorumController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public YorumController(AppDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> Sil(int id)
    {
        var yorum = await _context.Yorumlar.FindAsync(id);
        if (yorum == null)
            return NotFound();

        var currentUser = await _userManager.GetUserAsync(User);
        var isAdmin = User.IsInRole("Admin");

        if (yorum.AppUserId != currentUser.Id && !isAdmin)
            return Forbid();

        _context.Yorumlar.Remove(yorum);
        await _context.SaveChangesAsync();

        return RedirectToAction("Details", "Posts", new { id = yorum.PostId });
    }
}
