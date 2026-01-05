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
    public class ManageEducationController : Controller
    {
        private readonly AppDbContext _db;

        public ManageEducationController(AppDbContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            var list = await _db.Educations
                .OrderByDescending(e => e.StartDate)
                .ToListAsync();

            return View(list);
        }

        public IActionResult Create() => View(new Education());

        [HttpPost]
        public async Task<IActionResult> Create(Education model)
        {
            if (!ModelState.IsValid) return View(model);

            _db.Educations.Add(model);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long id)
        {
            var edu = await _db.Educations.FindAsync(id);
            return edu == null ? NotFound() : View(edu);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Education model)
        {
            if (!ModelState.IsValid) return View(model);

            _db.Educations.Update(model);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var edu = await _db.Educations.FindAsync(id);
            if (edu == null) return NotFound();

            _db.Educations.Remove(edu);
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
