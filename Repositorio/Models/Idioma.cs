using System;
using System.Collections.Generic;

#nullable disable

namespace Repositorio.Models
{
    public partial class Idioma
    {
        public Idioma()
        {
            Filmes = new HashSet<Filme>();
        }

        public int IdIdioma { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<Filme> Filmes { get; set; }
    }
}
