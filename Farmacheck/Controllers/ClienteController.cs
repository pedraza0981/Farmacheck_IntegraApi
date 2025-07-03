using Microsoft.AspNetCore.Mvc;

namespace Farmacheck.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
