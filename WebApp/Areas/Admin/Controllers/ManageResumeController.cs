using DataAccess;
using DataAccess.Vms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using QuestPDF.Fluent;
using WebApp.Controllers;
using WebApp.Helpers;
using WebApp.Pdf;
using WebApp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;

namespace WebApp.Areas.Admin.Controllers
{
    public class ManageResumeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStringLocalizer _localizer;
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public ManageResumeController(AppDbContext db, ILogger<HomeController> logger,
            IStringLocalizerFactory factory, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
            _db = db;
            var type = typeof(SharedResource);
            _localizer = factory.Create(type);
        }

        public async Task<IActionResult> Index()
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
