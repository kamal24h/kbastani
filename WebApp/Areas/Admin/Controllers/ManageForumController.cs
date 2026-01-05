using DataAccess;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ManageForumController : Controller
    {
        private readonly AppDbContext _db;
        public ManageForumController(AppDbContext db) => _db = db;

        public async Task<IActionResult> Categories()
        {
            var cats = await _db.ForumCategories.ToListAsync();
            return View(cats);
        }

        public IActionResult EditCategory(int? id)
        {
            if (id == null) return View(new ForumCategory());
            var cat = _db.ForumCategories.Find(id.Value);
            if (cat == null) return NotFound();
            return View(cat);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(ForumCategory model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ForumCategoryId == 0)
                _db.ForumCategories.Add(model);
            else
                _db.ForumCategories.Update(model);

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Categories));
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
