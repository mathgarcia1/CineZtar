using System;
using System.Collections.Generic;

#nullable disable

namespace Repositorio.Models
{
    public partial class Compra
    {
        public Compra()
        {
            CompraFilmes = new HashSet<CompraFilme>();
        }

        public int IdCompra { get; set; }
        public DateTime? Data { get; set; }
        public int? IdStatus { get; set; }
        public decimal? Valor { get; set; }
        public string IdPreferencia { get; set; }
        public string Url { get; set; }

        public virtual ICollection<CompraFilme> CompraFilmes { get; set; }
    }
}
