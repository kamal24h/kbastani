using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class PubicAboutController : Controller
{
    private readonly AppDbContext _db;
    public PubicAboutController(AppDbContext db)
    {
        _db = db;
    }

    // همه می‌توانند این صفحه را ببینند
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {        
        var about = await _db.Abouts.FirstOrDefaultAsync();
        return View(about);
    }
}
