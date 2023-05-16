using System;
using System.Collections.Generic;

#nullable disable

namespace Repositorio.Models
{
    public partial class Ingresso
    {
        public Ingresso()
        {
            Compras = new HashSet<Compra>();
        }

        public int IdIngresso { get; set; }
        public int? Numero { get; set; }
        public int? IdFilme { get; set; }
        public decimal? Valor { get; set; }

        public virtual Filme IdFilmeNavigation { get; set; }
        public virtual ICollection<Compra> Compras { get; set; }
    }
}
