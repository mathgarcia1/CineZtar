using AutoMapper;
using Microsoft.AspNetCore.WebUtilities;
using Repositorio.Models;
using Repositorio.Repositorios;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cine.Models
{
    public class TipoUsuarioModel
    {

        [KeyAttribute]
        [Required(ErrorMessage ="Código é obrigatório")]
        [Display(Name ="Código")]
        public int IdTipousuario { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatória")]
        [Display(Name = "Descrição")]
        [StringLength(maximumLength:50, ErrorMessage ="Máximo 50 Caracteres")]
        public string Descricao { get; set; }

        

        


    }
}
