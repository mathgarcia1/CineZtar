using System;
using System.Collections.Generic;

#nullable disable

namespace Repositorio.Models
{
    public partial class Filme
    {
        public Filme()
        {
            FilmeGeneros = new HashSet<FilmeGenero>();
            Sessaos = new HashSet<Sessao>();
        }

        public int IdFilme { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int? Duracao { get; set; }
        public string AnoLancamento { get; set; }
        public byte[] Imagem { get; set; }
        public int? IdIdioma { get; set; }

        public virtual Idioma IdIdiomaNavigation { get; set; }
        public virtual ICollection<FilmeGenero> FilmeGeneros { get; set; }
        public virtual ICollection<Sessao> Sessaos { get; set; }
    }
}
