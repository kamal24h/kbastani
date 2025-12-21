using DataAccess;
using DataAccess.Vms;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;

        public HomeController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
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
        public IActionResult SetLanguage(string culture, string returnUrl = "/")
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(
                    new RequestCulture(culture)
                ),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }

    //public class HomeController : Controller
    //{
    //    private readonly ILogger<HomeController> _logger;
    //    private readonly IStringLocalizer _localizer;
    //    private readonly AppDbContext _db;
    //    private readonly IWebHostEnvironment _env;

    //    public HomeController( AppDbContext db, ILogger<HomeController> logger,
    //        IStringLocalizerFactory factory, IWebHostEnvironment env)
    //    {
    //        _logger = logger;
    //        _db = db;
    //        var type = typeof(SharedResource);
    //        _localizer = factory.Create(type);
    //    }

    //    public IActionResult Index()
    //    {
    //        ViewData["Message"] = _localizer["Welcome"];
    //        return View();
    //    }       

    //    public IActionResult Privacy()
    //    {
    //        return View();
    //    }

    //    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    //    public IActionResult Error()
    //    {
    //        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    //    }
    //}
}
