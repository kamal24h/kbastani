using DataAccess.Vms;
using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;

public class PublicResumeController : Controller
{
    private readonly AppDbContext _db;
    private readonly IStringLocalizer<PublicResumeController> _localizer;
    public PublicResumeController(AppDbContext db, IStringLocalizer<PublicResumeController> localizer)
    {
        _db = db;
        _localizer = localizer;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var vm = new ResumeViewModel
        {
            Experiences = await _db.Experiences.OrderByDescending(e => e.StartDate).ToListAsync(),
            Educations = await _db.Educations.OrderByDescending(e => e.StartDate).ToListAsync(),
            Skills = System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName == "fa" ?
            await _db.Skills.OrderBy(s => s.NameFa).ToListAsync() :
            await _db.Skills.OrderBy(s => s.NameEn).ToListAsync()
        };
        ViewData["Title"] = _localizer["Resume"];
        return View(vm);
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
