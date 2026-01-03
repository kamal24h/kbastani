using DataAccess;
using DataAccess.Vms;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
     
namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ManageBlogController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public ManageBlogController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _db.BlogPosts
                .OrderByDescending(p => p.PublishedAt)
                .ToListAsync();

            return View(posts);
        }

        public IActionResult Create()
        {
            return View(new BlogPostViewModel());
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

        public async Task<IActionResult> Edit(int id)
        {
            var post = await _db.BlogPosts.FindAsync(id);
            if (post == null) return NotFound();

            return View(new BlogPostViewModel
            {
                BlogPostId = post.BlogPostId,
                TitleFa = post.TitleFa,
                TitleEn = post.TitleEn,
                SummaryFa = post.SummaryFa,
                SummaryEn = post.SummaryEn,
                ContentFa = post.ContentFa,
                ContentEn = post.ContentEn,
                IsPublished = post.IsPublished
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BlogPostViewModel model)
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

        

    }
}

