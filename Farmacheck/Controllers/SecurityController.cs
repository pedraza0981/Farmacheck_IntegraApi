using Farmacheck.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Farmacheck.Controllers
{
    public class SecurityController : Controller
    {
        private readonly IUserApiClient _userApiClient;

        public SecurityController(IUserApiClient userApiClient)
        {
            _userApiClient = userApiClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ChangePasswordModal()
        {
            return PartialView();
        }

        [HttpGet]
        public async Task<JsonResult> CheckPasswordUpdate(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return Json(new { success = false, error = "Correo electrónico no proporcionado." });
            }

            try
            {
                var user = await _userApiClient.GetUserByEmailAsync(email);
                if (user is null)
                {
                    return Json(new { success = false, error = "Usuario no encontrado." });
                }

                return Json(new { success = true, data = new { user.ActualizaPass } });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
    }
}
