using System;
using System.Collections.Generic;

#nullable disable

namespace Repositorio.Models
{
    public partial class Genero
    {
        public Genero()
        {
            Filmes = new HashSet<Filme>();
        }

        public int IdGenero { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Filme> Filmes { get; set; }
    }
}
