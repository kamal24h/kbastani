using DataAccess.Vms;
using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using Microsoft.AspNetCore.Localization;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ManageProjectController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public ManageProjectController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _db.Projects.ToListAsync());
        }

        public IActionResult Create() => View(new ProjectViewModel());

        [HttpPost]
        public async Task<IActionResult> Create(ProjectViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var project = new Project
            {
                TitleFa = model.TitleFa,
                TitleEn = model.TitleEn,
                DescriptionFa = model.DescriptionFa,
                DescriptionEn = model.DescriptionEn,
                ProjectUrl = model.ProjectUrl,
                GithubUrl = model.GithubUrl
            };

            if (model.Image != null)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(model.Image.FileName);
                string path = Path.Combine(_env.WebRootPath, "uploads/projects", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(path)!);

                using var stream = new FileStream(path, FileMode.Create);
                await model.Image.CopyToAsync(stream);

                project.ImagePath = "/uploads/projects/" + fileName;
            }

            _db.Projects.Add(project);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var project = await _db.Projects.FindAsync(id);
            if (project == null) return NotFound();

            return View(new ProjectViewModel
            {
                ProjectId = project.ProjectId,
                TitleFa = project.TitleFa,
                TitleEn = project.TitleEn,
                DescriptionFa = project.DescriptionFa,
                DescriptionEn = project.DescriptionEn,
                ProjectUrl = project.ProjectUrl,
                GithubUrl = project.GithubUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProjectViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var project = await _db.Projects.FindAsync(model.ProjectId);
            if (project == null) return NotFound();

            project.TitleFa = model.TitleFa;
            project.TitleEn = model.TitleEn;
            project.DescriptionFa = model.DescriptionFa;
            project.DescriptionEn = model.DescriptionEn;
            project.ProjectUrl = model.ProjectUrl;
            project.GithubUrl = model.GithubUrl;

            if (model.Image != null)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(model.Image.FileName);
                string path = Path.Combine(_env.WebRootPath, "uploads/projects", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(path)!);

                using var stream = new FileStream(path, FileMode.Create);
                await model.Image.CopyToAsync(stream);

                project.ImagePath = "/uploads/projects/" + fileName;
            }

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _db.Projects.FindAsync(id);
            if (project == null) return NotFound();

            _db.Projects.Remove(project);
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
