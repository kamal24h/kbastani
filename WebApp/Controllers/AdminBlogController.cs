using DataAccess;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static CommonUtility.MainMenu;
using System.Security.Claims;

namespace WebApp.Controllers
{
    
    // AdminBlogController (نمونه انتشار پست)
    [Authorize(Policy = "CanPublish")]
    public class AdminBlogController : Controller
    {
        private readonly AppDbContext _db;
        public AdminBlogController(AppDbContext db) => _db = db;

        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPost model)
        {
            if (!ModelState.IsValid) return View(model);
            model.AuthorId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            model.PublishedAt = DateTime.UtcNow;
            // تولید اسلاگ یکتا
            model.Slug = Slugify(model.TitleEn);
            _db.BlogPosts.Add(model);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Blog");
        }

        private string Slugify(string input)
        {
            // پیاده‌سازی ساده برای مثال
            var slug = input.ToLower().Replace(" ", "-");
            return slug; // در عمل: حذف کاراکترهای غیرمجاز و یکتا سازی
        }
    }    
}
