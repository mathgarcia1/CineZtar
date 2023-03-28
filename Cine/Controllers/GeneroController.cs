using AutoMapper;
using Cine.Models;
using Microsoft.AspNetCore.Mvc;
using Repositorio.Models;
using Repositorio.Repositorios;

namespace Cine.Controllers
{
    public class GeneroController : BaseController<Genero, GeneroModel>
    {
        public GeneroController(IBaseRepository<Genero> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        protected override int GetId(Genero entity)
        {
            return entity.IdGenero;
        }
    }
}
