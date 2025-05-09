using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EisntMvc.Models.ViewModels;

public class ProdutosViewModel
{
    [Required(ErrorMessage = "O SKU é obrigatório.")]
    public string Sku { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "A descrição é obrigatória.")]
    public string Descricao { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Quantidade deve ser maior ou igual a zero.")]
    public int Qtd { get; set; }

    [Required(ErrorMessage = "A categoria é obrigatória.")]
    public int Categoria_Id { get; set; }

    [Required(ErrorMessage = "O preço é obrigatório.")]
    public decimal Preco { get; set; }

    [Required(ErrorMessage = "O fornecedor é obrigatório.")]
    public int Fornecedor_Id { get; set; }

    // Listas para popular dropdowns
    public List<ProdutosModel> Produtos { get; set; } = new List<ProdutosModel>();
    public List<CategoriaModel> Categorias { get; set; } = new List<CategoriaModel>();
    public List<FornecedorModel> Fornecedores { get; set; } = new List<FornecedorModel>();
}
