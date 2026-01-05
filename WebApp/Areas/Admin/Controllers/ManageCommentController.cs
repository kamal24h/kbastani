using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ManageCommentController : Controller
    {
        private readonly AppDbContext _db;

        public ManageCommentController(AppDbContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            var comments = await _db.Comments
                .Include(c => c.User)
                .Include(c => c.Post)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            return View(comments);
        }

        [HttpPost]
        public async Task<IActionResult> Approve(long id)
        {
            var comment = await _db.Comments.FindAsync(id);
            if (comment == null) return NotFound();

            comment.IsApproved = true;
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Reject(long id)
        {
            var comment = await _db.Comments.FindAsync(id);
            if (comment == null) return NotFound();

            comment.IsApproved = false;
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var comment = await _db.Comments.FindAsync(id);
            if (comment == null) return NotFound();

            _db.Comments.Remove(comment);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult SetLanguage(string culture, string returnUrl = "/")
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1),
                    IsEssential = true
                });
            return RedirectToAction("Index");
        }
    }

}
