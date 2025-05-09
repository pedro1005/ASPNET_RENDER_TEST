using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EisntMvc.Models;

public class CategoriaModel
{
    [Key]
    public int Id { get; set; }
    
    public string Nome { get; set; }
}