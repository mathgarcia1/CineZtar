using System;
using System.Collections.Generic;

#nullable disable

namespace Repositorio.Models
{
    public partial class SessaoIngresso
    {
        public int IdSessaoIngresso { get; set; }
        public decimal? Preco { get; set; }
        public int? IdSessao { get; set; }
        public int? IdCarrinho { get; set; }
        public int? IdIngresso { get; set; }
        public string Status { get; set; }

        public virtual Carrinho IdCarrinhoNavigation { get; set; }
        public virtual Ingresso IdIngressoNavigation { get; set; }
        public virtual Sessao IdSessaoNavigation { get; set; }
    }
}
