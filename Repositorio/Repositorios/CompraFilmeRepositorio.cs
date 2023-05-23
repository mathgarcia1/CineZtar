using Repositorio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Repositorios
{
    public class CompraFilmeRepositorio : BaseRepositorio<CompraFilme>
    {
        public CompraFilmeRepositorio(DB_Ingressos2Context contexto) :base(contexto)
        {
        }
    }
}
