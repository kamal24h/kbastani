using DataAccess;
using DataAccess.Vms;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using QuestPDF.Fluent;
using System.Diagnostics;
using WebApp.Helpers;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("/Action")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStringLocalizer _localizer;
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public HomeController( AppDbContext db, ILogger<HomeController> logger,
            IStringLocalizerFactory factory, IWebHostEnvironment env)
        {
            _logger = logger;
            _db = db;
            var type = typeof(SharedResource);
            _localizer = factory.Create(type);
        }


        public IActionResult Index()
        {
            ViewData["Message"] = _localizer["Welcome"];
            return View();
        }

        [HttpGet("resume")]
        public async Task<IActionResult> Resume()
        {
            var vm = new ResumeViewModel
            {
                Skills = await _db.Skills.OrderByDescending(s => s.Level).ToListAsync(),
                Projects = await _db.Projects.OrderByDescending(p => p.CreatedAt).ToListAsync(),
                Experiences = await _db.Experiences.OrderByDescending(e => e.StartDate).ToListAsync(),
                Educations = await _db.Educations.OrderByDescending(e => e.StartDate).ToListAsync()
            };

            return View(vm);
        }

        public async Task<IActionResult> ExportPdf()
        {
            var vm = await BuildResumeViewModel();

            var culture = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

            var document = new ResumePdfDocument(vm, culture);
            var pdfBytes = document.GeneratePdf();   // متد استاندارد تولید PDF

            var fileName = culture == "fa" ? "رزومه.pdf" : "resume.pdf";

            return File(pdfBytes, "application/pdf", fileName);
        }

        public async Task<IActionResult> ExportAtsPdf()
        {
            var vm = await BuildResumeViewModel();

            var document = new ResumeAtsPdfDocument(vm);
            var pdfBytes = document.GeneratePdf();

            return File(pdfBytes, "application/pdf", "resume-ATS.pdf");
        }

        [HttpGet("about")]
        public async Task<IActionResult> About()
        {
            var about = await _db.Abouts.FirstOrDefaultAsync();
            return View(about);
        }

        public async Task<IActionResult> EditAbout()
        {
            var about = await _db.Abouts.FirstOrDefaultAsync() ?? new About();

            return View(new AboutViewModel
            {
                AboutId = about.AboutId,
                TitleFa = about.TitleFa,
                TitleEn = about.TitleEn,
                BioFa = about.BioFa,
                BioEn = about.BioEn,
                Email = about.Email,
                Phone = about.Phone,
                LocationFa = about.LocationFa,
                LocationEn = about.LocationEn,
                LinkedinUrl = about.LinkedinUrl,
                GithubUrl = about.GithubUrl,
                WebsiteUrl = about.WebsiteUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditAbout(AboutViewModel model)
        {
            var about = await _db.Abouts.FirstOrDefaultAsync();

            if (about == null)
            {
                about = new About();
                _db.Abouts.Add(about);
            }

            about.TitleFa = model.TitleFa;
            about.TitleEn = model.TitleEn;
            about.BioFa = model.BioFa;
            about.BioEn = model.BioEn;
            about.Email = model.Email;
            about.Phone = model.Phone;
            about.LocationFa = model.LocationFa;
            about.LocationEn = model.LocationEn;
            about.LinkedinUrl = model.LinkedinUrl;
            about.GithubUrl = model.GithubUrl;
            about.WebsiteUrl = model.WebsiteUrl;

            if (model.ProfileImage != null)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(model.ProfileImage.FileName);
                string path = Path.Combine(_env.WebRootPath, "uploads/profile", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(path)!);

                using var stream = new FileStream(path, FileMode.Create);
                await model.ProfileImage.CopyToAsync(stream);

                about.ProfileImagePath = "/uploads/profile/" + fileName;
            }

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        
        private async Task<ResumeViewModel> BuildResumeViewModel()
        {
            return new ResumeViewModel
            {
                Skills = await _db.Skills.OrderByDescending(s => s.Level).ToListAsync(),
                Projects = await _db.Projects.OrderByDescending(p => p.CreatedAt).ToListAsync(),
                Experiences = await _db.Experiences.OrderByDescending(e => e.StartDate).ToListAsync(),
                Educations = await _db.Educations.OrderByDescending(e => e.StartDate).ToListAsync(),

                // اگر پروفایل را از جدول جدا می‌خوانی، اینجا لودش کن
                FullNameFa = "کمال ...",
                FullNameEn = "Kamal ...",
                JobTitleFa = "توسعه‌دهنده ارشد ASP.NET Core",
                JobTitleEn = "Senior ASP.NET Core Developer",
                BioFa = "متن معرفی فارسی...",
                BioEn = "English bio text..."
            };
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
