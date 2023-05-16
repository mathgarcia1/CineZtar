using System;
using System.Collections.Generic;

#nullable disable

namespace Repositorio.Models
{
    public partial class Compra
    {
        public int IdCompra { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? Data { get; set; }
        public int? QtdIngressos { get; set; }
        public int? IdIngresso { get; set; }
        public int? IdStatus { get; set; }
        public decimal? Valor { get; set; }

        public virtual Ingresso IdIngressoNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
