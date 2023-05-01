using System;
using System.Collections.Generic;

#nullable disable

namespace Repositorio.Models
{
    public partial class Sala
    {
        public Sala()
        {
            Ingressos = new HashSet<Ingresso>();
        }

        public int IdSala { get; set; }
        public string Nome { get; set; }
        public int? TotalAssento { get; set; }
        public int? IdCinema { get; set; }

        public virtual Cinema IdCinemaNavigation { get; set; }
        public virtual ICollection<Ingresso> Ingressos { get; set; }
    }
}
