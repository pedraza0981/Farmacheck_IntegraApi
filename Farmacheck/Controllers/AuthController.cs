using AutoMapper;
using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Security;
using Farmacheck.Application.DTOs;
using Farmacheck.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Farmacheck.Controllers;

public class AuthController : Controller
{
    private readonly IAuthApiClient _apiClient;
    private readonly IMapper _mapper;
    private readonly IUserApiClient _userApiClient;

    public AuthController(IAuthApiClient apiClient, IMapper mapper, IUserApiClient userApiClient)
    {
        _apiClient = apiClient;
        _mapper = mapper;
        _userApiClient = userApiClient;
    }

    [HttpGet]
    public async Task<JsonResult> Validate()
    {
        Request.Headers.TryGetValue("Authorization", out var authorizationHeader);
        if (!string.IsNullOrEmpty(authorizationHeader))
        {
            var token = authorizationHeader.First().Replace("Bearer ", string.Empty);
            var result = await _apiClient.ValidateAsync(token);
            return Json(new { success = true, data = result });
        }
        return Json(new { success = false, error = "Token not provided" });
    }

    [HttpGet("user")]
    public async Task<JsonResult> GetUser()
    {
        Request.Headers.TryGetValue("Authorization", out var authorizationHeader);
        if (!string.IsNullOrEmpty(authorizationHeader))
        {
            var token = authorizationHeader.First().Replace("Bearer ", string.Empty);
            var info = await _apiClient.GetUserInfoAsync(token);
            var dto = _mapper.Map<UserInfoDto>(info);
            var model = _mapper.Map<UserInfoViewModel>(dto);
            return Json(new { success = true, data = model });
        }
        return Json(new { success = false, error = "Token not provided" });
    }

    [HttpPost]
    public async Task<JsonResult> Login([FromBody] CredentialsRequest model)
    {
        try
        {
            var response = await _apiClient.LoginAsync(model);
            var dto = _mapper.Map<TokenDto>(response);
            var vm = _mapper.Map<TokenViewModel>(dto);

            if (string.IsNullOrEmpty(vm?.Token))
            {
                return Json(new { success = false, error = "Credenciales incorrectas, favor de verificar." });
            }

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = vm.Expiration
            };

            Response.Cookies.Append("AuthToken", vm.Token, cookieOptions);

            return Json(new { success = true, data = vm });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, error = ex.Message });
        }
    }

    [HttpGet]
    public async Task<JsonResult> GetUserByEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return Json(new { success = false, error = "Correo electrónico requerido" });
        }

        try
        {
            var user = await _userApiClient.GetUserByEmailAsync(email);
            if (user == null)
            {
                return Json(new { success = false, error = "Usuario no encontrado" });
            }

            return Json(new { success = true, data = new { actualizaPass = user.ActualizaPass } });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, error = ex.Message });
        }
    }

    [HttpPut]
    public async Task<JsonResult> Refresh([FromBody] RegenerateRequest model)
    {
        try
        {
            var response = await _apiClient.RefreshAsync(model);
            var dto = _mapper.Map<TokenDto>(response);
            var vm = _mapper.Map<TokenViewModel>(dto);
            return Json(new { success = true, data = vm });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, error = ex.Message });
        }
    }

    [HttpDelete]
    public async Task<JsonResult> Logout()
    {
        Request.Headers.TryGetValue("Authorization", out var authorizationHeader);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Path = "/"
        };

        if (!string.IsNullOrEmpty(authorizationHeader))
        {
            var token = authorizationHeader.First().Replace("Bearer ", string.Empty);
            var result = await _apiClient.LogoutAsync(token);
            Response.Cookies.Delete("AuthToken", cookieOptions);
            return Json(new { success = true, data = result });
        }

        Response.Cookies.Delete("AuthToken", cookieOptions);
        return Json(new { success = false, error = "Token not provided" });
    }
}
