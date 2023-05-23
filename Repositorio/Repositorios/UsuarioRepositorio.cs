using Microsoft.EntityFrameworkCore;
using Repositorio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Repositorios
{
    public class UsuarioRepositorio : BaseRepositorio<Usuario>
    {
        public UsuarioRepositorio(DB_Ingressos2Context contexto) : base(contexto)
        {
        }

        // public Usuario RecuperarPorId(int IdUsuario)
        // {
        //     return _contexto.Usuarios.FirstOrDefault(u => u.IdUsuario == IdUsuario);
        // }
    }
}
