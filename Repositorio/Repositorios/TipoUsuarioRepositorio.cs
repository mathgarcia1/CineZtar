using Repositorio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Repositorios
{
    public class TipoUsuarioRepositorio : BaseRepositorio<TipoUsuario>
    {
        public TipoUsuarioRepositorio(DB_Ingressos2Context contexto) : base(contexto)
        {
        }
    }
}
