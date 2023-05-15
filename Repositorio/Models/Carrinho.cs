using System;
using System.Collections.Generic;

#nullable disable

namespace Repositorio.Models
{
    public partial class Carrinho
    {
        public int IdCarrinho { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? Data { get; set; }
        public int? QtdIngressos { get; set; }
        public int? IdIngresso { get; set; }

        public virtual Ingresso IdIngressoNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
