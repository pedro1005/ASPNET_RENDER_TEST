using EisntMvc.Models;
using Microsoft.AspNetCore.Mvc;
using EisntMvc.Repositorio;
using Microsoft.AspNetCore.Authorization;

namespace EisntMvc.Controllers;

public class CategoriasController : Controller
{
    private readonly IRepositorioProdutos _categoriaRepositorio;

    public CategoriasController(IRepositorioProdutos categoriaRepositorio)
    {
        _categoriaRepositorio = categoriaRepositorio;
    }
    // GET
    public IActionResult Index()
    {
        List<CategoriaModel> categorias = _categoriaRepositorio.ProcurarCategorias();
        return View(categorias);
    }

    public IActionResult Criar()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Criar(CategoriaModel novacategoria)
    {
        if (!ModelState.IsValid)
        {
            Console.WriteLine("Ocorreu um erro ao criar a categoria");
            return View(novacategoria);
        }
        _categoriaRepositorio.AdicionarCategoria(novacategoria);
        return RedirectToAction("Index");
    }
    
    public IActionResult EditarCategoria(int id)
    {
        CategoriaModel categoria = _categoriaRepositorio.ListarCatPorId(id);
        if (!ModelState.IsValid)
        {
            return View("Index");
        }
        return View(categoria);
    }
    
    [HttpPost]
    public IActionResult Alterar(CategoriaModel categoria)
    {
        if (!ModelState.IsValid)
        {
            Console.WriteLine("Ocorreu um erro ao alterar a categoria.");
            return View("EditarCategoria", categoria);
        }
        _categoriaRepositorio.AtualizarCat(categoria);
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult ConfApagarCategoria(int id)
    {
        var categoria = _categoriaRepositorio.ListarCatPorId(id);
        return View(categoria);
    }

    [HttpPost]
    public IActionResult ApagarCategoria(int id)
    {
        if ((_categoriaRepositorio.ExcluirCat(id)) == 0)
        {
            TempData["PopupMensagem"] = "Não foi possível excluir a categoria.";
            return RedirectToAction("PopupError");
        }

        return RedirectToAction("Index");
    }
    
    public IActionResult PopupError()
    {
        return View();
    }
}

