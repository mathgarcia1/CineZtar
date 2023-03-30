using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cine.Models
{
    public class SalaModel
    {

        [KeyAttribute]
        [Required(ErrorMessage = "Código é obrigatório")]
        [Display(Name = "Código")]
        public int IdSala { get; set; }

        [Required(ErrorMessage = "Nome da sala é obrigatório!")]
        [Display(Name = "Nome da Sala")]
        [StringLength(maximumLength:50, ErrorMessage = "Máximo 50 Caractéres")]
        public string Nome { get; set; }

        [Display(Name = "Total de assentos")]
        public int? TotalAssento { get; set; }

        [Display(Name = "Código do Cinema")]
        public int? IdCinema { get; set; }

        public List<SelectListItem> Cinema { get; set; }
    }
}
