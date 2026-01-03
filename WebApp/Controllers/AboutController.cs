using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using WebApp;

public class AboutController : Controller
{
    private readonly AppDbContext _db;
    private readonly IStringLocalizer<SharedResource> _localizer;

    public AboutController(AppDbContext db, IStringLocalizer<SharedResource> localizer)
    {
        _db = db;
        _localizer = localizer;
    }

    // همه می‌توانند این صفحه را ببینند
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {        
        var about = await _db.Abouts.FirstOrDefaultAsync();
        ViewData["Title"] = _localizer["Contact"];
        return View(about);
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
