using System;
using System.Collections.Generic;

#nullable disable

namespace Repositorio.Models
{
    public partial class Sessao
    {
        public Sessao()
        {
            Carrinhos = new HashSet<Carrinho>();
            SessaoIngressos = new HashSet<SessaoIngresso>();
        }

        public int IdSessao { get; set; }
        public DateTime? Data { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public int? IdFilme { get; set; }
        public int? IdSala { get; set; }

        public virtual Filme IdFilmeNavigation { get; set; }
        public virtual Sala IdSalaNavigation { get; set; }
        public virtual ICollection<Carrinho> Carrinhos { get; set; }
        public virtual ICollection<SessaoIngresso> SessaoIngressos { get; set; }
    }
}
