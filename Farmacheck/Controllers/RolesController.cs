using Microsoft.AspNetCore.Mvc;

namespace Farmacheck.Controllers
{
    public class RolesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
