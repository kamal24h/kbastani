using System.Diagnostics;
using kbastani.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using WebApp.Helpers;

namespace kbastani.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStringLocalizer _localizer;

        public HomeController(ILogger<HomeController> logger, IStringLocalizerFactory factory)
        {
            _logger = logger;
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
