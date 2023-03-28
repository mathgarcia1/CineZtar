using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repositorio.Models;
using Repositorio.Repositorios;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cine.Models
{
    public class UsuarioModel
    {
        [KeyAttribute]
        [Required(ErrorMessage = "Código é obrigatório")]
        [Display(Name = "Código")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório")]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [Display(Name = "Nome")]
        [StringLength(maximumLength: 50, ErrorMessage = "Máximo 50 Caracteres")]
        public string Nome { get; set; }


        [Display(Name = "Tipo de Usuario")]
        public int IdTipousuario { get; set; }


        public List<SelectListItem> TiposUsuario { get; set; }

    }
}
