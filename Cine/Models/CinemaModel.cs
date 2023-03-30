using System.ComponentModel.DataAnnotations;

namespace Cine.Models
{
    public class CinemaModel
    {
        [KeyAttribute]
        [Required(ErrorMessage = "Código é obrigatório!")]
        [Display(Name = "Código")]
        public int IdCinema { get; set; }

        [Required(ErrorMessage = "Nome do cinema é obrigatório!")]
        [Display(Name = "Nome")]
        [StringLength(maximumLength:50, ErrorMessage = "Máximo 50 Caractéres")]
        public string Nome { get; set; }

        public int? TotalSalaCinema { get; set; }

        
    }
}
