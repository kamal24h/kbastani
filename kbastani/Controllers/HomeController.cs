namespace WebApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Localization;

    public class HomeController : Controller
    {
        private readonly IStringLocalizer _localizer;
        public HomeController(IStringLocalizerFactory factory)
        {
            var type = typeof(SharedResource);
            _localizer = factory.Create(type);
        }

        public IActionResult Index()
        {
            ViewData["Message"] = _localizer["Welcome"];
            return View();
        }
    }

    public class SharedResource { }

}
