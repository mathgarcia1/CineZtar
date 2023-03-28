using AutoMapper;
using Cine.Models;
using Microsoft.AspNetCore.Mvc;
using Repositorio.Models;
using Repositorio.Repositorios;

namespace Cine.Controllers
{
    public class IdiomaController : BaseController<Idioma, IdiomaModel>
    {
        public IdiomaController(IBaseRepository<Idioma> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        protected override int GetId(Idioma entity)
        {
            return entity.IdIdioma;
        }
    }
}
