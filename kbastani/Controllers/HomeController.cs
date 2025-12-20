using DataAccess;
using DataAccess.Vms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Diagnostics;
using WebApp.Helpers;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStringLocalizer _localizer;
        private readonly AppDbContext _db;

        public HomeController( AppDbContext db, ILogger<HomeController> logger, IStringLocalizerFactory factory)
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

        public IActionResult Portfolio()
        {
            return View();
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
