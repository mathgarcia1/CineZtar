using System;
using System.Collections.Generic;

#nullable disable

namespace Repositorio.Models
{
    public partial class Carrinho
    {
        public Carrinho()
        {
            SessaoIngressos = new HashSet<SessaoIngresso>();
        }

        public int IdCarrinho { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdSessao { get; set; }
        public DateTime? Data { get; set; }
        public int? QtdIngressos { get; set; }

        public virtual Sessao IdSessaoNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
        public virtual ICollection<SessaoIngresso> SessaoIngressos { get; set; }
    }
}
