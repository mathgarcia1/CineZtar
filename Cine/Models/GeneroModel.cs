using System.ComponentModel.DataAnnotations;

namespace Cine.Models
{
    public class GeneroModel
    {
        [KeyAttribute]
        [Required(ErrorMessage = "Código é obrigatório!")]
        [Display(Name = "Código")]
        public int IdGenero { get; set; }

        [Required(ErrorMessage = "Nome do gênero é obrigatório!")]
        [Display(Name = "Gênero")]
        [StringLength(maximumLength:50, ErrorMessage = "Máximo 50 Caractéres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Descrição do gênero é obrigatório!")]
        [Display(Name = "Descrição")]
        [StringLength(maximumLength: 150, ErrorMessage = "Máximo 150 Caractéres")]
        public string Descricao { get; set; }

    }
}
