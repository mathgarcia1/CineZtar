using AutoMapper;
using Cine.Models;
using Microsoft.AspNetCore.Mvc;
using Repositorio.Models;
using Repositorio.Repositorios;

namespace Cine.Controllers
{
    public class TipoIngressoController : BaseController<TipoIngresso, TipoIngressoModel>
    {
        public TipoIngressoController(IBaseRepository<TipoIngresso> repository, IMapper mapper) : base(repository, mapper)
        { 

        }

    
        protected override int GetId(TipoIngresso entity)
        {
            return entity.IdTipoIngresso;
        }
    }
}
