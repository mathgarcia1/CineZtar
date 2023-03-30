using System.ComponentModel.DataAnnotations;

namespace Cine.Models
{
    public class IngressoModel
    {

        [KeyAttribute]
        [Required(ErrorMessage = "Código é obrigatório")]
        [Display(Name = "Código")]
        public int IdIngresso { get; set; }

        [Display(Name = "Número")]
        public int? Numero { get; set; }


        [Display(Name = "Código Tipo Ingresso")]
        public int? IdTipoIngresso { get; set; }

//falta index disso
        [Display(Name = "Código Sala")]
        public int? IdSala { get; set; }
    }
}
