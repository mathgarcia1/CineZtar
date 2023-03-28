using AutoMapper;
using Cine.Models;
using Microsoft.AspNetCore.Mvc;
using Repositorio.Models;
using Repositorio.Repositorios;

namespace Cine.Controllers
{
    public class IngressoController : BaseController<Ingresso, IngressoModel>
    {
        public IngressoController(IBaseRepository<Ingresso> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        protected override int GetId(Ingresso entity)
        {
            return entity.IdIngresso;
        }
    }
}
