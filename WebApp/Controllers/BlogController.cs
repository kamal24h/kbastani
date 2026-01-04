using DataAccess;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Security.Claims;

namespace WebApp.Controllers
{
    [AllowAnonymous]
    public class BlogController : Controller
    {
        private readonly AppDbContext _db;
        public BlogController(AppDbContext db) => _db = db;

        // List of posts
        public async Task<IActionResult> Index(string? category, string? tag, int page = 1)
        {
            var query = _db.BlogPosts
                .Where(p => p.IsPublished)
                .Include(p => p.Category)
                .Include(p => p.Tags).ThenInclude(t => t.Tag)
                .OrderByDescending(p => p.PublishedAt);

            //if (categoryId.HasValue)
            //    query = query.Where(b => b.CategoryId == categoryId);

            //if (!string.IsNullOrWhiteSpace(tag)) 
            //    query = query.Where(b => b.Tags.Any(t => t.Tag.NameEn == tag || t.Tag.NameFa == tag));

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
            .Include(b => b.Category)
            .Include(b => b.Author)
            .Include(b => b.Comments)
            .ThenInclude(c => c.User)
            .FirstOrDefaultAsync(b => b.Slug == slug && b.IsPublished);

            if (post == null) return NotFound();
            return View(post);
        }


        //[Authorize(Roles = $"{MainRoles.Admin},{MainRoles.Editor}")]
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

        [HttpPost]
        [Authorize] // فقط کاربران لاگین شده
        public async Task<IActionResult> AddComment(long postId, string body)
        {
            if (string.IsNullOrWhiteSpace(body))
                return BadRequest("Comment cannot be empty");
            var post = await _db.BlogPosts.FindAsync(postId);
            if (post == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            
            var comment = new Comment
            {
                PostId = postId,
                Body = body,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                IsApproved = false // 🔥 باید توسط Admin تأیید شود
            }; 
            _db.Comments.Add(comment); 
            await _db.SaveChangesAsync(); 
            return RedirectToAction("Details", new { slug = post.Slug }); 
        }

        public async Task<IActionResult> Search(string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                return RedirectToAction("Index");

            var culture = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

            IQueryable<BlogPost> query = _db.BlogPosts
                .Where(p => p.IsPublished);

            // Search in FA
            if (culture == "fa")
            {
                query = query.Where(p =>
                    p.TitleFa.Contains(q) ||
                    p.SummaryFa.Contains(q) ||
                    p.ContentFa.Contains(q));
            }
            else // Search in EN
            {
                query = query.Where(p =>
                    p.TitleEn.Contains(q) ||
                    p.SummaryEn.Contains(q) ||
                    p.ContentEn.Contains(q));
            }

            var results = await query
                .OrderByDescending(p => p.PublishedAt)
                .ToListAsync();

            ViewBag.Query = q;

            //query = query.Where(p =>
            //p.Tags.Any(t => t.Tag.NameFa.Contains(q) || t.Tag.NameEn.Contains(q))
            //);


            //query = query.Where(p =>
            //p.Category.NameFa.Contains(q) || p.Category.NameEn.Contains(q)
            //);


            return View(results);
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


