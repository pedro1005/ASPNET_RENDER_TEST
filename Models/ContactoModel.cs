using Microsoft.EntityFrameworkCore;

namespace EisntMvc.Models;

public class ContactoModel
{
    public string Assunto { get; set; }
    public string Email { get; set; }
    public string Mensagem { get; set; }
}