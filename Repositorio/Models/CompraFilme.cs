using System;
using System.Collections.Generic;

#nullable disable

namespace Repositorio.Models
{
    public partial class CompraFilme
    {
        public int IdCompraFilme { get; set; }
        public int? Quantidade { get; set; }
        public decimal? Valor { get; set; }
        public int? IdCompra { get; set; }
        public int? IdFilme { get; set; }

        //compras
        public virtual Compra IdCompraNavigation { get; set; }
        //produto
        public virtual Filme IdFilmeNavigation { get; set; }
    }
}
