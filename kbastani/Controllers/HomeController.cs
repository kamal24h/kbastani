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
