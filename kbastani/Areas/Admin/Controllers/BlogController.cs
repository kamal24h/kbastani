using DataAccess;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _db;
        public BlogController(AppDbContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            var posts = await _db.BlogPosts
                .OrderByDescending(p => p.PublishedAt)
                .ToListAsync();
            return View(posts);
        }

        public IActionResult Create()
        {
            return View(new BlogPost());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPost model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.PublishedAt = DateTime.UtcNow;
            _db.BlogPosts.Add(model);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var post = await _db.BlogPosts.FindAsync(id);
            if (post == null) return NotFound();
            return View(post);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BlogPost model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _db.BlogPosts.Update(model);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> TogglePublish(int id)
        {
            var post = await _db.BlogPosts.FindAsync(id);
            if (post == null) return NotFound();
            post.IsPublished = !post.IsPublished;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
