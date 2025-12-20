using DataAccess;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TagController : Controller
    {
        private readonly AppDbContext _db;

        public TagController(AppDbContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            return View(await _db.Tags.ToListAsync());
        }

        public IActionResult Create() => View(new Tag());

        [HttpPost]
        public async Task<IActionResult> Create(Tag model)
        {
            if (!ModelState.IsValid) return View(model);

            model.Slug = model.NameEn.ToLower().Replace(" ", "-");
            _db.Tags.Add(model);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var tag = await _db.Tags.FindAsync(id);
            return tag == null ? NotFound() : View(tag);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Tag model)
        {
            if (!ModelState.IsValid) return View(model);

            model.Slug = model.NameEn.ToLower().Replace(" ", "-");
            _db.Tags.Update(model);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }

}
