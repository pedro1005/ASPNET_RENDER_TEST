using Microsoft.AspNetCore.Mvc;
using EisntMvc.Models;
using EisntMvc.Repositorio;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using EisntMvc.Data;
using EisntMvc.Models.ViewModels;

namespace EisntMvc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosApiController : ControllerBase
    {
        private readonly IRepositorioProdutos _produtoRepositorio;

        public ProdutosApiController(IRepositorioProdutos produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        // Rota API: GET /api/ProdutosApi
        [HttpGet]
        public IActionResult GetProdutos()
        {
            var produtos = _produtoRepositorio.ProcurarTodos();
            return Ok(produtos);
        }

        // Rota API: GET /api/ProdutosApi/{id}
        [HttpGet("{id}")]
        public IActionResult GetProduto(int id)
        {
            var produto = _produtoRepositorio.ListarPorId(id);
            if (produto == null)
                return NotFound(new { message = "Produto não encontrado" });
            return Ok(produto);
        }
    }
    public class GestaoProdutosController : Controller
    {
        private readonly IRepositorioProdutos _produtoRepositorio;

        public GestaoProdutosController(IRepositorioProdutos produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        // GET: GestaoProdutosController
        public IActionResult Index()
        {
            var viewModel = new ProdutosViewModel
            {
                Produtos = _produtoRepositorio.ProcurarTodos(),
                Categorias = _produtoRepositorio.ProcurarCategorias(),
                Fornecedores = _produtoRepositorio.ProcurarFornecedores()
            };
            return View(viewModel);
        }

        public IActionResult Criar()
        {
            var viewModel = new ProdutosViewModel
            {
                Produtos = _produtoRepositorio.ProcurarTodos(),
                Categorias = _produtoRepositorio.ProcurarCategorias(),
                Fornecedores = _produtoRepositorio.ProcurarFornecedores()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Criar(ProdutosViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Mapear o ViewModel para o Model
                    var novoProduto = new ProdutosModel
                    {
                        Sku = model.Sku,
                        Nome = model.Nome,
                        Descricao = model.Descricao,
                        Qtd = model.Qtd,
                        Preco = model.Preco,
                        Categoria_Id = model.Categoria_Id,
                        Fornecedor_Id = model.Fornecedor_Id
                    };
                    _produtoRepositorio.Adicionar(novoProduto);
                    TempData["MensagemSucesso"] = "Produto inserido com sucesso";
                    return RedirectToAction("Index");
                }
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = "Erro ao inserir produto";
            }
            model.Categorias = _produtoRepositorio.ProcurarCategorias();
            model.Fornecedores = _produtoRepositorio.ProcurarFornecedores();
            return View(model);
        }

        public IActionResult Editar(int id)
        {
            ProdutosModel produto = _produtoRepositorio.ListarPorId(id);
            if (produto == null)
            {
                return NotFound();
            }
            ViewBag.Fornecedores = _produtoRepositorio.ProcurarFornecedores();
            ViewBag.Categorias = _produtoRepositorio.ProcurarCategorias();
            return View(produto);
        }

        [HttpPost]
        public IActionResult Alterar(ProdutosModel produto)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Ocorreu um erro ao alterar o produto");
                return View("Editar", produto);
            }
            _produtoRepositorio.Atualizar(produto);
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public IActionResult ApagarConfirmacao(int id)
        {
            var produto = _produtoRepositorio.ListarPorId(id);
            return View(produto);
        }

        [HttpPost]
        public IActionResult Apagar(int id)
        {
            try
            {

                int apagado = _produtoRepositorio.Excluir(id);
                if (apagado == 1)
                    TempData["MensagemSucesso"] = "Produto excluido com sucesso";
                else
                {
                    TempData["MensagemErro"] = "Ocorreu um erro ao excluir";
                }

                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = "Erro ao excluir";
                //return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}


