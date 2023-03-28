using System.ComponentModel.DataAnnotations;

namespace Cine.Models
{
    public class TipoIngressoModel
    {
        [KeyAttribute]
        [Required(ErrorMessage = "Código é obrigatório")]
        [Display(Name = "Código")]
        public int IdTipoIngresso { get; set; }

        [Required(ErrorMessage = "Nome é obrigatória")]
        [Display(Name = "Nome")]
        [StringLength(maximumLength: 50, ErrorMessage = "Máximo 50 Caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatória")]
        [Display(Name = "Descrição")]
        [StringLength(maximumLength: 50, ErrorMessage = "Máximo 50 Caracteres")]
        public string Descricao { get; set; }
    }
}
