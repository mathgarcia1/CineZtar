using System;
using System.Collections.Generic;

#nullable disable

namespace Repositorio.Models
{
    public partial class Cinema
    {
        public Cinema()
        {
            Salas = new HashSet<Sala>();
        }

        public int IdCinema { get; set; }
        public int? TotalSalaCinema { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<Sala> Salas { get; set; }
    }
}
