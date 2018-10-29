using System.ComponentModel.DataAnnotations;

namespace Projeto01.Areas.Seguranca.Models
{
    public class UsuarioViewModel
    {
        public string Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }        
    }
}