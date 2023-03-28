using System.ComponentModel.DataAnnotations;

namespace Cine.Models
{
    public class IdiomaModel
    {
        [KeyAttribute]
        [Required(ErrorMessage = "Código é obrigatório!")]
        [Display(Name = "Código")]
        public int IdIdioma { get; set; }

        [Required(ErrorMessage = "Nome do idioma é obrigatório!")]
        [Display(Name = "Idioma")]
        [StringLength(maximumLength:50, ErrorMessage = "Máximo 50 Caractéres")]
        public string Nome { get; set; }

        
    }
}
