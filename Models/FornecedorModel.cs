using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EisntMvc.Models;

public class FornecedorModel
{
    [Key]
    public int Id { get; set; }
    
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public string Morada { get; set; }
}