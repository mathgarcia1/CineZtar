using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cine.Models
{
    public class FilmeModel
    {
        [KeyAttribute]
        [Required(ErrorMessage = "Código é obrigatório!")]
        [Display(Name = "Código")]
        public int IdFilme { get; set; }

        [Required(ErrorMessage = "Nome do filme é obrigatório!")]
        [Display(Name = "Nome")]
        [StringLength(maximumLength:50, ErrorMessage = "Máximo 50 Caractéres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Descrição do filme é obrigatório!")]
        [Display(Name = "Descrição")]
        [StringLength(maximumLength:50, ErrorMessage = "Máximo 50 Caractéres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Duração do filme é obrigatório!")]
        [Display(Name = "Duração")]
        public int? Duracao { get; set; }

        [Required(ErrorMessage = "Ano de lançamento do filme é obrigatório!")]
        [Display(Name = "Ano de lançamento")]
        [StringLength(maximumLength:50, ErrorMessage = "Máximo 50 Caractéres")]
        public string AnoLancamento { get; set; }
        
        public byte[] Imagem { get; set; }
        
        [Display(Name = "Idioma")]
        public int? IdIdioma { get; set; }

        

        public List<SelectListItem> IdiomasFilme;

        [Display(Name = "Imagem")]

        public IFormFile ImagemUpload{get; set;}
    }
}
