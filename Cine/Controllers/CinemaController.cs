using AutoMapper;
using Cine.Models;
using Microsoft.AspNetCore.Mvc;
using Repositorio.Models;
using Repositorio.Repositorios;

namespace Cine.Controllers
{
    public class CinemaController : BaseController<Cinema, CinemaModel>
    {
        public CinemaController(IBaseRepository<Cinema> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        protected override int GetId(Cinema entity)
        {
            return entity.IdCinema;
        }
    }
}