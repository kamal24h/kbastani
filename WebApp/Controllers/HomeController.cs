using DataAccess;
using DataAccess.Vms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using WebApp;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _db;
    private readonly IStringLocalizer<SharedResource> _localizer;

    public HomeController(ILogger<HomeController> logger,
        AppDbContext db, IStringLocalizer<SharedResource> localizer)
    {
        _logger = logger;
        _db = db;
        _localizer = localizer;
    }

    public async Task<IActionResult> IndexAsync()
    {
        ViewData["Title"] = _localizer["Home"];

        //return RedirectToAction("MyTest");
        var vm = new HomeViewModel
        {
            About = await _db.Abouts.FirstOrDefaultAsync(),

            LatestPosts = await _db.BlogPosts
                .Where(p => p.IsPublished)
                .OrderByDescending(p => p.PublishedAt)
                .Take(3)
                .ToListAsync(),

            LatestProjects = await _db.Projects
                .OrderByDescending(p => p.CreatedAt)
                .Take(3)
                .ToListAsync()
        };

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
        return RedirectToAction("IndexAsync");
    }

    public IActionResult MyTest()
    {
        ViewData["Title"] = _localizer["Home"];
        ViewData["Second"] = _localizer["Education"];
        return View();
    }


}


