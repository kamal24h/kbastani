using DataAccess;
using DataAccess.Vms;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ManageAboutController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public ManageAboutController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var about = await _db.Abouts.FirstOrDefaultAsync();
            return View(about);
        }

        public async Task<IActionResult> Edit()
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
        public async Task<IActionResult> Edit(AboutViewModel model)
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
