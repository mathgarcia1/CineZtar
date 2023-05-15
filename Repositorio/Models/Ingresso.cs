using System;
using System.Collections.Generic;

#nullable disable

namespace Repositorio.Models
{
    public partial class Ingresso
    {
        public Ingresso()
        {
            Carrinhos = new HashSet<Carrinho>();
        }

        public int IdIngresso { get; set; }
        public int? Numero { get; set; }
        public int? IdTipoIngresso { get; set; }
        public int? IdSala { get; set; }

        public virtual Sala IdSalaNavigation { get; set; }
        public virtual TipoIngresso IdTipoIngressoNavigation { get; set; }
        public virtual ICollection<Carrinho> Carrinhos { get; set; }
    }
}
