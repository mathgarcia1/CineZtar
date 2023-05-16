using Microsoft.EntityFrameworkCore;
using Repositorio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Repositorios
{
    public class IngressoRepositorio : BaseRepository<Ingresso>
    {
        private readonly DbContext _context;

        public IngressoRepositorio()
        {
        }

        public IngressoRepositorio(DB_IngressosContext contexto)
        {
        }
    }
}
