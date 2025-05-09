using EisntMvc.Models;
using Microsoft.AspNetCore.Mvc;
using EisntMvc.Repositorio;

namespace EisntMvc.Controllers;

public class FornecedoresController : Controller
{
    private readonly IRepositorioProdutos _fornecedorRepositorio;

    public FornecedoresController(IRepositorioProdutos fornecedorRepositorio)
    {
        _fornecedorRepositorio = fornecedorRepositorio;
    }
    // GET
    public IActionResult Index()
    {
        List<FornecedorModel> fornecedores = _fornecedorRepositorio.ProcurarFornecedores();
        return View(fornecedores);
    }
    
    public IActionResult Criar()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Criar(FornecedorModel novofornecedor)
    {
        if (!ModelState.IsValid)
        {
            Console.WriteLine("Ocorreu um erro ao criar o fornecedor");
            return View(novofornecedor);
        }
        _fornecedorRepositorio.AdicionarFornecedor(novofornecedor);
        return RedirectToAction("Index");
    }
    
    public IActionResult EditarFornecedor(int id)
    {
        var fornecedor = _fornecedorRepositorio.GetFornecedor(id);
        return View(fornecedor);
    }
    
    [HttpPost]
    public IActionResult AlterarFornecedor(FornecedorModel fornecedor)
    {
        if (!ModelState.IsValid)
        {
            Console.WriteLine("Ocorreu um erro ao alterar o fornecedor.");
            return View("EditarFornecedor", fornecedor);
        }
        _fornecedorRepositorio.AtualizarFornecedor(fornecedor);
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult ConfApagarFornecedor(int id)
    {
        var fornecedor = _fornecedorRepositorio.GetFornecedor(id);
        return View(fornecedor);
    }

    [HttpPost]
    public IActionResult ApagarFornecedor(int id)
    {
        _fornecedorRepositorio.ExcluirFornecedor(id);
        return RedirectToAction("Index");
    }
}