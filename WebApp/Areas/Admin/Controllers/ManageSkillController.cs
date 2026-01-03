using DataAccess;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ManageSkillController : Controller
    {
        private readonly AppDbContext _db;

        public ManageSkillController(AppDbContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            return View(await _db.Skills.ToListAsync());
        }

        public IActionResult Create() => View(new Skill());

        [HttpPost]
        public async Task<IActionResult> Create(Skill model)
        {
            if (!ModelState.IsValid) return View(model);

            _db.Skills.Add(model);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var skill = await _db.Skills.FindAsync(id);
            return skill == null ? NotFound() : View(skill);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Skill model)
        {
            if (!ModelState.IsValid) return View(model);

            _db.Skills.Update(model);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var skill = await _db.Skills.FindAsync(id);
            if (skill == null) return NotFound();

            _db.Skills.Remove(skill);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }

}
