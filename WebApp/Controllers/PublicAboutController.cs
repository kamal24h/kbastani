using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using WebApp;

public class PublicAboutController : Controller
{
    private readonly AppDbContext _db;
    private readonly IStringLocalizer<SharedResource> _localizer;

    public PublicAboutController(AppDbContext db, IStringLocalizer<SharedResource> localizer)
    {
        _db = db;
        _localizer = localizer;
    }

    // همه می‌توانند این صفحه را ببینند
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {        
        var about = await _db.Abouts.FirstOrDefaultAsync();
        ViewData["Title"] = _localizer["Home"];
        return View(about);
    }
}
