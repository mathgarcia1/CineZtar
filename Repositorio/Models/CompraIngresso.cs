using System;
using System.Collections.Generic;

#nullable disable

namespace Repositorio.Models
{
    public partial class CompraIngresso
    {
        public int IdCompraIngresso { get; set; }
        public int? Quantidade { get; set; }
        public decimal? Valor { get; set; }
        public int? IdCompra { get; set; }
        public int? IdIngresso { get; set; }
    }
}
