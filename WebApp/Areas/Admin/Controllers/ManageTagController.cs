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
    public class ManageTagController : Controller
    {
        private readonly AppDbContext _db;

        public ManageTagController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var tags = await _db.Tags
                .OrderBy(t => t.NameEn)
                .ToListAsync();

            return View(tags);
        }

        public IActionResult Create()
        {
            return View(new Tag());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tag model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.Slug = model.NameEn.ToLower().Replace(" ", "-");

            _db.Tags.Add(model);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var tag = await _db.Tags.FindAsync(id);
            if (tag == null)
                return NotFound();

            return View(tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Tag model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var tag = await _db.Tags.FindAsync(model.TagId);
            if (tag == null)
                return NotFound();

            tag.NameFa = model.NameFa;
            tag.NameEn = model.NameEn;
            tag.Slug = model.NameEn.ToLower().Replace(" ", "-");

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var tag = await _db.Tags.FindAsync(id);
            if (tag == null)
                return NotFound();

            _db.Tags.Remove(tag);
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
    //[Area("Admin")]
    //[Authorize(Roles = "Admin")]
    //public class TagController : Controller
    //{
    //    private readonly AppDbContext _db;

    //    public TagController(AppDbContext db) => _db = db;

    //    public async Task<IActionResult> Index()
    //    {
    //        return View(await _db.Tags.ToListAsync());
    //    }

    //    public IActionResult Create() => View(new Tag());

    //    [HttpPost]
    //    public async Task<IActionResult> Create(Tag model)
    //    {
    //        if (!ModelState.IsValid) return View(model);

    //        model.Slug = model.NameEn.ToLower().Replace(" ", "-");
    //        _db.Tags.Add(model);
    //        await _db.SaveChangesAsync();

    //        return RedirectToAction(nameof(Index));
    //    }

    //    public async Task<IActionResult> Edit(int id)
    //    {
    //        var tag = await _db.Tags.FindAsync(id);
    //        return tag == null ? NotFound() : View(tag);
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> Edit(Tag model)
    //    {
    //        if (!ModelState.IsValid) return View(model);

    //        model.Slug = model.NameEn.ToLower().Replace(" ", "-");
    //        _db.Tags.Update(model);
    //        await _db.SaveChangesAsync();

    //        return RedirectToAction(nameof(Index));
    //    }
    //}
}


