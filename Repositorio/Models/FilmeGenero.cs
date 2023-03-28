using System;
using System.Collections.Generic;

#nullable disable

namespace Repositorio.Models
{
    public partial class FilmeGenero
    {
        public int IdFilmeGenero { get; set; }
        public int? IdGenero { get; set; }
        public int? IdFilme { get; set; }

        public virtual Filme IdFilmeNavigation { get; set; }
        public virtual Genero IdGeneroNavigation { get; set; }
    }
}
