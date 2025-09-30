using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Parking.Controllers
{
    [Authorize(Roles = "Apprentice")]
    public class DashboardAprendizController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
