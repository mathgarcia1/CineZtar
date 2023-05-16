using System;
using System.Collections.Generic;

#nullable disable

namespace Repositorio.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Compras = new HashSet<Compra>();
        }

        public int IdUsuario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public int IdTipousuario { get; set; }

        public virtual TipoUsuario IdTipousuarioNavigation { get; set; }
        public virtual ICollection<Compra> Compras { get; set; }
    }
}
