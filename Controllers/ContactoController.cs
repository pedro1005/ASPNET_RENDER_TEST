using System.Runtime.InteropServices;
using EisntMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace EisntMvc.Controllers;

public class ContactoController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Enviar(ContactoModel formulario)
    {
        return View(formulario);
    }
}