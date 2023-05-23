using System;
using System.Collections.Generic;

#nullable disable

namespace Repositorio.Models
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public int IdTipousuario { get; set; }

        public virtual TipoUsuario IdTipousuarioNavigation { get; set; }
    }
}
