using DataAccess;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static CommonUtility.MainMenu;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    // ForumController (نمونه)
    [AllowAnonymous]
    public class PublicForumController : Controller
    {
        private readonly AppDbContext _db;
        public PublicForumController(AppDbContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            var cats = await _db.ForumCategories
                .Include(c => c.Threads)
                .ToListAsync();
            return View(cats);
        }

        [Authorize]
        public IActionResult CreateThread(int categoryId) => View(new ForumThread { CategoryId = categoryId });

        [Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateThread(ForumThread model, string body)
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(body)) return View(model);
            model.AuthorId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            _db.ForumThreads.Add(model);
            await _db.SaveChangesAsync();

            var post = new ForumPost
            {
                ThreadId = model.ForumThreadId,
                Body = body.Trim(),
                AuthorId = model.AuthorId
            };
            _db.ForumPosts.Add(post);
            await _db.SaveChangesAsync();
            return RedirectToAction("Thread", new { id = model.ForumThreadId });
        }

        public async Task<IActionResult> Thread(int id)
        {
            var thread = await _db.ForumThreads
                .Include(t => t.Posts).ThenInclude(p => p.Author)
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.ForumThreadId == id);
            if (thread == null) return NotFound();
            return View(thread);
        }

        [Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Reply(int threadId, string body)
        {
            var thread = await _db.ForumThreads.FindAsync(threadId);
            if (thread == null || thread.IsLocked) return BadRequest();
            var post = new ForumPost
            {
                ThreadId = threadId,
                Body = body.Trim(),
                AuthorId = User.FindFirstValue(ClaimTypes.NameIdentifier)!
            };
            _db.ForumPosts.Add(post);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Thread), new { id = threadId });
        }
    }

}
