using System;
using System.Collections.Generic;

#nullable disable

namespace Repositorio.Models
{
    public partial class Filme
    {
        public Filme()
        {
            CompraFilmes = new HashSet<CompraFilme>();
        }

        public int IdFilme { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int? Duracao { get; set; }
        public string AnoLancamento { get; set; }
        public string Imagem { get; set; }
        public int? IdIdioma { get; set; }
        public decimal? Valor { get; set; }
        public int? IdGenero { get; set; }

        public virtual Genero IdGeneroNavigation { get; set; }
        public virtual Idioma IdIdiomaNavigation { get; set; }
        public virtual ICollection<CompraFilme> CompraFilmes { get; set; }
    }
}
