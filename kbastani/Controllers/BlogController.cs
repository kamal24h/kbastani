using DataAccess;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace WebApp.Controllers
{
    [AllowAnonymous]
    public class BlogController : Controller
    {
        private readonly AppDbContext _db;
        public BlogController(AppDbContext db) => _db = db;

        public async Task<IActionResult> Index(string? category, string? tag, int page = 1)
        {
            var query = _db.BlogPosts
                .Where(p => p.IsPublished)
                .Include(p => p.Category)
                .Include(p => p.Tags).ThenInclude(t => t.Tag)
                .OrderByDescending(p => p.PublishedAt);

            if (!string.IsNullOrEmpty(category))
                query = (IOrderedQueryable<BlogPost>)query.Where(p => p.Category != null && p.Category.Slug == category);
            if (!string.IsNullOrEmpty(tag))
                query = (IOrderedQueryable<BlogPost>)query.Where(p => p.Tags.Any(t => t.Tag.Slug == tag));

            var posts = await query.Skip((page - 1) * 10).Take(10).ToListAsync();
            return View(posts);
        }

        public async Task<IActionResult> Details(string slug)
        {
            var post = await _db.BlogPosts
                .Include(p => p.Comments).ThenInclude(c => c.User)
                .FirstOrDefaultAsync(p => p.Slug == slug && p.IsPublished);
            if (post == null) return NotFound();
            return View(post);
        }

        [Authorize(Policy = "CanComment"), HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(int postId, string body)
        {
            if (string.IsNullOrWhiteSpace(body))
            {
                ModelState.AddModelError("", "متن نظر ضروری است.");
            }
            var post = await _db.BlogPosts.FindAsync(postId);
            if (post == null || !post.IsPublished) return NotFound();

            var comment = new Comment
            {
                Body = body.Trim(),
                PostId = postId,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!,
                IsApproved = true // یا false در صورت نیاز به تایید
            };
            _db.Comments.Add(comment);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { slug = post.Slug });
        }
    }
}