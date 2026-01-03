using DataAccess;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ManageCategoryController : Controller
    {
        private readonly AppDbContext _db;

        public ManageCategoryController(AppDbContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            return View(await _db.BlogCategories.ToListAsync());
        }

        public IActionResult Create() => View(new BlogCategory());

        [HttpPost]
        public async Task<IActionResult> Create(BlogCategory model)
        {
            if (!ModelState.IsValid) return View(model);

            model.Slug = model.NameEn.ToLower().Replace(" ", "-");
            _db.BlogCategories.Add(model);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var cat = await _db.BlogCategories.FindAsync(id);
            return cat == null ? NotFound() : View(cat);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BlogCategory model)
        {
            if (!ModelState.IsValid) return View(model);

            model.Slug = model.NameEn.ToLower().Replace(" ", "-");
            _db.BlogCategories.Update(model);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }

}
