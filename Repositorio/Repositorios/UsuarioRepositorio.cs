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
    public class UsuarioRepositorio : BaseRepository<Usuario>
    {
        private readonly DbContext _context;


        public Usuario GetByEmailAndSenha(String email, String senha)
        {
            return _context.Set<Usuario>().SingleOrDefault(u => u.Email == email && u.Senha == senha);
        }
        
    }
}
