using AutoMapper;
using Cine.Models;
using Microsoft.AspNetCore.Mvc;
using Repositorio.Models;
using Repositorio.Repositorios;
using System;
using System.Collections.Generic;

namespace Cine.Controllers
{
    public class TipoUsuarioController : BaseController<TipoUsuario, TipoUsuarioModel>
    {
        public TipoUsuarioController(IBaseRepository<TipoUsuario> repository, IMapper mapper) : base(repository, mapper)
        {
        }
        protected override int GetId(TipoUsuario entity)
        {
            return entity.IdTipousuario;
        }
        

    }
}
