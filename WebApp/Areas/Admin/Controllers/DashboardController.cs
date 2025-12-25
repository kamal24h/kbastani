using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            var culture = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

            if (culture == "fa")
                ViewBag.AdminLayout = "_AdminLayout.fa.cshtml";
            else
                ViewBag.AdminLayout = "_AdminLayout.en.cshtml";
            return View();
        }
    }
}
