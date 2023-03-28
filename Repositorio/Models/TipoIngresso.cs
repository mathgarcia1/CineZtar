using System;
using System.Collections.Generic;

#nullable disable

namespace Repositorio.Models
{
    public partial class TipoIngresso
    {
        public TipoIngresso()
        {
            Ingressos = new HashSet<Ingresso>();
        }

        public int IdTipoIngresso { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Ingresso> Ingressos { get; set; }
    }
}
