using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace EisntMvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
public class AccountController : Controller
{
    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        if (username == "admin" && password == "1234") // Simulação de login
        {
            HttpContext.Session.SetString("Autenticado", "true");
            HttpContext.Session.SetString("Username", username);
            return Json(new { success = true });
        }
        return Json(new { success = false });
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Remove("Autenticado");
        return RedirectToAction("Index", "Home");
    }
}
