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
        public CompraFilmeRepositorio(DB_Ingressos2Context contexto) : base(contexto)
        {   
            // _contexto = contexto;
        }
        

        // public void RemoverFilmeDaCompra(int idCompraFilme, int idFilme)
        // {
        //     var compraFilme = _contexto.CompraFilmes.FirstOrDefault(c => c.IdCompraFilme == idCompraFilme && c.IdFilme == idFilme);
        //     if (compraFilme != null)
        //     {
        //         _contexto.CompraFilmes.Remove(compraFilme);
        //         _contexto.SaveChanges();
        //     }
        // }
    }
}
