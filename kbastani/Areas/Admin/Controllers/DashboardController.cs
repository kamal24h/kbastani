using Microsoft.AspNetCore.Authorization;

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController
    {
    }
}
