using EisntMvc.Data;
using EisntMvc.Models;
using System.Collections.Generic;
using System.Linq;
using EisntMvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc;
using EisntMvc.Controllers;

namespace EisntMvc.Repositorio;

public class RepositorioProdutos : IRepositorioProdutos
{
    private readonly EisntDbContext _bancoContext;

    public RepositorioProdutos(EisntDbContext bancoContext)
    {
        _bancoContext = bancoContext;
    }
    public ProdutosModel Adicionar(ProdutosModel produto)
    {
        _bancoContext.Produtos.Add(produto);
        _bancoContext.SaveChanges();

        return produto;
    }

    public void AdicionarCategoria(CategoriaModel categoria)
    {
        _bancoContext.CategoriaModel.Add(categoria);
        _bancoContext.SaveChanges();
    }

    public void AdicionarFornecedor(FornecedorModel fornecedor)
    {
        _bancoContext.FornecedorModel.Add(fornecedor);
        _bancoContext.SaveChanges();
    }
    
    public void Atualizar(ProdutosModel produto)
    {
        if (produto == null)
            return;
        //var produtoDB = _bancoContext.Produtos.FirstOrDefault(p => p.Id == produto.Id);
        _bancoContext.Produtos.Update(produto);
        _bancoContext.SaveChanges();
        

        /*if (produtoDB != null)
        {
            produtoDB.Sku = produto.Sku;
            produtoDB.Nome = produto.Nome;
            produtoDB.Descricao = produto.Descricao;
            produtoDB.Qtd = produto.Qtd;
            produtoDB.Preco = produto.Preco;
            produtoDB.Fornecedor_Id = produto.Fornecedor_Id;
            produtoDB.Categoria_Id = produto.Categoria_Id;

            _bancoContext.Produtos.Update(produtoDB);
            _bancoContext.SaveChanges();
        }*/
    }

    public void AtualizarCat(CategoriaModel categoria)
    {
        var categoriaDb = _bancoContext.CategoriaModel.FirstOrDefault(c => c.Id == categoria.Id);
        if (categoriaDb == null)
            return;
        categoriaDb.Nome = categoria.Nome;
        _bancoContext.CategoriaModel.Update(categoriaDb);
        _bancoContext.SaveChanges();
    }

    public void AtualizarFornecedor(FornecedorModel fornecedor)
    {
        var fornecedorDb = _bancoContext.FornecedorModel.FirstOrDefault(p => p.Id == fornecedor.Id);
        if (fornecedorDb == null)
            return;
        fornecedorDb.Nome = fornecedor.Nome;
        fornecedorDb.Email = fornecedor.Email;
        fornecedorDb.Telefone = fornecedor.Telefone;
        fornecedorDb.Morada = fornecedor.Morada;
        _bancoContext.FornecedorModel.Update(fornecedorDb);
        _bancoContext.SaveChanges();
    }

    public int Excluir(int id)
    {
        var produtoDB = _bancoContext.Produtos.FirstOrDefault(p => p.Id == id);

        if (produtoDB != null)
        {
            _bancoContext.Produtos.Remove(produtoDB);
            _bancoContext.SaveChanges();
            return (1);
        }

        return (0);
    }

    public int ExcluirCat(int id)
    {
        var catDb = _bancoContext.CategoriaModel.FirstOrDefault(c => c.Id == id);
        var produtos = _bancoContext.Produtos.Where(p => p.Categoria_Id == id);
        if (catDb == null || produtos.Any())
            return 0;

        _bancoContext.CategoriaModel.Remove(catDb);
        _bancoContext.SaveChanges();
        return 1;
    }
    
    public void ExcluirFornecedor(int id)
    {
        var fornecedorDb = _bancoContext.FornecedorModel.FirstOrDefault(p => p.Id == id);
        var produtos = _bancoContext.Produtos.Where(p => p.Fornecedor_Id == id);

        if (fornecedorDb == null || produtos.Any())
        {
            return;
        }

        _bancoContext.FornecedorModel.Remove(fornecedorDb);
        _bancoContext.SaveChanges();
    }


    public ProdutosModel ListarPorId(int id)
    {
        return _bancoContext.Produtos
            .FirstOrDefault(p => p.Id == id);
    }

    public CategoriaModel ListarCatPorId(int id)
    {
        return _bancoContext.CategoriaModel
            .FirstOrDefault(c => c.Id == id);
    }

    public FornecedorModel GetFornecedor(int id)
    {
        return _bancoContext.FornecedorModel
            .FirstOrDefault(f => f.Id == id);
    }

    public List<ProdutosModel> ProcurarTodos()
    {
        return _bancoContext.Produtos.ToList();
    }
    
    public List<CategoriaModel> ProcurarCategorias()
    {
        return _bancoContext.CategoriaModel.ToList();
    }
    
    public List<FornecedorModel> ProcurarFornecedores()
    {
        return _bancoContext.FornecedorModel.ToList();
    }
}
