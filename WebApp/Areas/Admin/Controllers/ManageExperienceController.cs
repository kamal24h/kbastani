using DataAccess;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ManageExperienceController : Controller
    {
        private readonly AppDbContext _db;

        public ManageExperienceController(AppDbContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            var list = await _db.Experiences
                .OrderByDescending(e => e.StartDate)
                .ToListAsync();

            return View(list);
        }

        public IActionResult Create() => View(new Experience());

        [HttpPost]
        public async Task<IActionResult> Create(Experience model)
        {
            if (!ModelState.IsValid) return View(model);

            _db.Experiences.Add(model);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long id)
        {
            var exp = await _db.Experiences.FindAsync(id);
            return exp == null ? NotFound() : View(exp);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Experience model)
        {
            if (!ModelState.IsValid) return View(model);

            _db.Experiences.Update(model);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var exp = await _db.Experiences.FindAsync(id);
            if (exp == null) return NotFound();

            _db.Experiences.Remove(exp);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }

}
