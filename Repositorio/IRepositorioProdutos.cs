using EisntMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace EisntMvc.Repositorio;

public interface IRepositorioProdutos
{
    ProdutosModel Adicionar(ProdutosModel produto);
    void AdicionarCategoria(CategoriaModel categoria);
    void AdicionarFornecedor(FornecedorModel fornecedor);
    
    void Atualizar(ProdutosModel produto);
    
    void AtualizarCat(CategoriaModel categoria);
    void AtualizarFornecedor(FornecedorModel fornecedor);
    
    int Excluir(int id);
    int ExcluirCat(int id);
    void ExcluirFornecedor(int id);
    
    
    ProdutosModel ListarPorId(int id);
    
    CategoriaModel ListarCatPorId(int id);
    FornecedorModel GetFornecedor(int id);
    List<ProdutosModel> ProcurarTodos();
    List<CategoriaModel> ProcurarCategorias();
    List<FornecedorModel> ProcurarFornecedores();
}