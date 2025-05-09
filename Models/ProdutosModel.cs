using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EisntMvc.Models;

public class ProdutosModel
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O SKU é obrigatório.")]
    [MaxLength(50)]
    public string Sku { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MaxLength(50)]
    public string Nome { get; set; }

    [MaxLength(250)]
    [Required(ErrorMessage = "A descriçao é obrigatória.")]
    public string Descricao { get; set; }

    public int Qtd { get; set; }

    [Required(ErrorMessage = "A categoria é obrigatória.")]
    [ForeignKey("Categoria")]
    public int Categoria_Id { get; set; }

    [Required(ErrorMessage = "O preço é obrigatório.")]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Preco { get; set; }

    [Required(ErrorMessage = "O fornecedor é obrigatório.")]
    [ForeignKey("Fornecedor")]
    public int Fornecedor_Id { get; set; }
}
