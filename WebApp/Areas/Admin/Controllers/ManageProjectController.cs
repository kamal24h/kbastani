using DataAccess.Vms;
using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using Microsoft.AspNetCore.Localization;
using Service.Contract;
using Service;
using DataAccess.Dtos;
using Azure.Identity;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ManageProjectController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IProjectService _projectService;

        public ManageProjectController(IWebHostEnvironment env, IProjectService projectService)
        {
            _env = env;
            _projectService = projectService;
        }

        public async Task<IActionResult> Index()
        {
            var projects = await _projectService.Get();
            return View(projects);
        }

        public IActionResult Create() => View(new ProjectDto());

        [HttpPost]
        public async Task<IActionResult> Create(ProjectDto model)
        {
            if (!ModelState.IsValid) return View(model);

            //var project = new Project
            //{
            //    TitleFa = model.TitleFa,
            //    TitleEn = model.TitleEn,
            //    DescriptionFa = model.DescriptionFa,
            //    DescriptionEn = model.DescriptionEn,
            //    ProjectUrl = model.ProjectUrl,
            //    GithubUrl = model.GithubUrl
            //};

            /// todo: Kamal

            //if (model.ImagePath != null)
            //{
            //    string fileName = Guid.NewGuid() + Path.GetExtension(model.ImagePath.FileName);
            //    string path = Path.Combine(_env.WebRootPath, "uploads/projects", fileName);

            //    Directory.CreateDirectory(Path.GetDirectoryName(path)!);

            //    using var stream = new FileStream(path, FileMode.Create);
            //    await model.Image.CopyToAsync(stream);

            //    project.ImagePath = "/uploads/projects/" + fileName;
            //}

            await _projectService.AddAsync(model);            
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var project = await _projectService.GetByIdAsync(id);
            if (project == null) return NotFound();

            return View(new ProjectDto
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

            var project = await _projectService.GetByIdAsync(model.ProjectId);
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

            //await _db.SaveChangesAsync();
            await _projectService.UpdateAsync(new ProjectDto
            {
                ProjectId = project.ProjectId,
                TitleFa = project.TitleFa,
                TitleEn = project.TitleEn,
                DescriptionFa = project.DescriptionFa,
                DescriptionEn = project.DescriptionEn,
                ProjectUrl = project.ProjectUrl,
                GithubUrl = project.GithubUrl
            }
            );

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
           if (id == 0) return NotFound();
           var result = await _projectService.DeleteById(id);          
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
