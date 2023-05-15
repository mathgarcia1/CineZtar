using System;
using System.ComponentModel.DataAnnotations;

namespace Cine.Models
{
    public class CarrinhoModel
    {
        [KeyAttribute]
        [Required(ErrorMessage = "Código é obrigatório!")]
        [Display(Name = "Código")]
        public int IdCarrinho { get; set; }
        public int? IdUsuario { get; set; }

        public DateTime? Data { get; set; }
        
        [Display(Name = "Quantidade de Ingressos")]
        public int? QtdIngressos { get; set; }
        [Display(Name = "Ingressos")]
        public int? IdIngresso { get; set; }
                
        public virtual IngressoModel IdIngressoNavigation { get; set; }
        public virtual UsuarioModel IdUsuarioNavigation { get; set; }
    }
}
