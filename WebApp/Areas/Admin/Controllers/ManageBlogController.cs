using DataAccess;
using DataAccess.Vms;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Helpers;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ManageBlogController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<AppUser> _userManager;

        public ManageBlogController(AppDbContext db, IWebHostEnvironment env, UserManager<AppUser> userManager)
        {
            _db = db;
            _env = env;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _db.BlogPosts
                .Include(b => b.Category)
                .Include(b => b.Author)
                .Include(b => b.Comments.Where(c => c.IsApproved))
                .OrderByDescending(b => b.PublishedAt)
                .ToListAsync(); 
            return View(posts);
        }

        public IActionResult Create()
        {
            var mod = new BlogPostViewModel();
            mod.Categories = _db.BlogCategories.ToList();
            mod.Tags = _db.Tags.ToList();
            //ViewBag.Categories = _db.BlogCategories.ToList();
            return View(mod);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPostViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var post = new BlogPost
            {
                TitleFa = model.TitleFa,
                TitleEn = model.TitleEn,
                SummaryFa = model.SummaryFa,
                SummaryEn = model.SummaryEn,
                ContentFa = model.ContentFa,
                ContentEn = model.ContentEn,
                IsPublished = model.IsPublished,
                Slug = GenerateSlug(model.TitleEn)
            };

            // Upload Image
            if (model.Thumbnail != null)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(model.Thumbnail.FileName);
                string path = Path.Combine(_env.WebRootPath, "uploads/blog", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(path)!);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.Thumbnail.CopyToAsync(stream);
                }

                post.ThumbnailPath = "/uploads/blog/" + fileName;
            }

            foreach (var tagId in model.SelectedTags)
            {
                post.Tags.Add(new BlogPostTag
                {
                    TagId = tagId
                });
            }

            _db.BlogPosts.Add(post);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create1(BlogPost model, IFormFile? thumbnail)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _db.BlogCategories.ToList();
                return View(model);
            }

            model.BlogPostGuid = Guid.NewGuid();
            model.Slug = SlugHelper.GenerateSlug(model.TitleEn);

            if (thumbnail != null)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(thumbnail.FileName)}";
                var path = Path.Combine("wwwroot/uploads/blog", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                using var stream = new FileStream(path, FileMode.Create);
                await thumbnail.CopyToAsync(stream);

                model.ThumbnailPath = fileName;
            }

            var user = await _userManager.GetUserAsync(User);
            model.AuthorId = user!.Id;

            _db.BlogPosts.Add(model);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long id)
        {
            var post = await _db.BlogPosts.FindAsync(id);
            if (post == null) return NotFound();

            ViewBag.Categories = _db.BlogCategories.ToList();
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BlogPost model, IFormFile? thumbnail)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _db.BlogCategories.ToList();
                return View(model);
            }

            var post = await _db.BlogPosts.FindAsync(model.BlogPostId);
            if (post == null) return NotFound();

            post.TitleFa = model.TitleFa;
            post.TitleEn = model.TitleEn;
            post.SummaryFa = model.SummaryFa;
            post.SummaryEn = model.SummaryEn;
            post.ContentFa = model.ContentFa;
            post.ContentEn = model.ContentEn;
            post.CategoryId = model.CategoryId;
            post.IsPublished = model.IsPublished;
            post.PublishedAt = model.PublishedAt;
            post.Slug = SlugHelper.GenerateSlug(post.TitleEn);

            if (thumbnail != null)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(thumbnail.FileName)}";
                var path = Path.Combine("wwwroot/uploads/blog", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                using var stream = new FileStream(path, FileMode.Create);
                await thumbnail.CopyToAsync(stream);

                post.ThumbnailPath = fileName;
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit1(BlogPostViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var post = await _db.BlogPosts.FindAsync(model.BlogPostId);
            if (post == null) return NotFound();

            post.TitleFa = model.TitleFa;
            post.TitleEn = model.TitleEn;
            post.SummaryFa = model.SummaryFa;
            post.SummaryEn = model.SummaryEn;
            post.ContentFa = model.ContentFa;
            post.ContentEn = model.ContentEn;
            post.IsPublished = model.IsPublished;

            // Upload new image
            if (model.Thumbnail != null)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(model.Thumbnail.FileName);
                string path = Path.Combine(_env.WebRootPath, "uploads/blog", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(path)!);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.Thumbnail.CopyToAsync(stream);
                }

                post.ThumbnailPath = "/uploads/blog/" + fileName;
            }

            _db.BlogPostTags.RemoveRange(post.Tags);

            foreach (var tagId in model.SelectedTags)
            {
                post.Tags.Add(new BlogPostTag
                {
                    PostId = post.BlogPostId,
                    TagId = tagId
                });
            }

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

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _db.BlogPosts.FindAsync(id);
            if (post == null) return NotFound();

            _db.BlogPosts.Remove(post);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private string GenerateSlug(string title)
        {
            return title.ToLower().Replace(" ", "-").Replace(".", "");
        }

        public async Task<IActionResult> Category(string slug)
        {
            var posts = await _db.BlogPosts
                .Include(p => p.Category)
                .Where(p => p.Category.Slug == slug && p.IsPublished)
                .ToListAsync();

            return View("Index", posts);
        }

        public async Task<IActionResult> Tag(string slug)
        {
            var posts = await _db.BlogPostTags
                .Where(t => t.Tag.Slug == slug)
                .Select(t => t.Post)
                .Where(p => p.IsPublished)
                .ToListAsync();

            return View("Index", posts);
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