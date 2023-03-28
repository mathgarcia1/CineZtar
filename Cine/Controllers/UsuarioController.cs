using AutoMapper;
using Cine.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositorio.Models;
using Repositorio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cine.Controllers
{
    public class UsuarioController : BaseController<Usuario, UsuarioModel>
    {
        private readonly IBaseRepository<TipoUsuario> _tipoUsuarioRepository;
        //private readonly IBaseRepository<Usuario> _usuarioRepository;

        public UsuarioController(IBaseRepository<Usuario> usuarioRepository, IBaseRepository<TipoUsuario> tipoUsuarioRepository, IMapper mapper)
            : base(usuarioRepository, mapper)
        {

            //_usuarioRepository = usuarioRepository;
            _tipoUsuarioRepository = tipoUsuarioRepository;
        }

        protected override int GetId(Usuario entity)
        {
            return entity.IdUsuario;
        }

        public override IActionResult Index(int? id)
        {
            //ViewBag.TiposUsuario = _tipoUsuarioRepository.getAll().ToList();
            //return base.Index(id);

            var model = new UsuarioModel();
            var tiposUsuario = _tipoUsuarioRepository.getAll().
                Select(tipo => new SelectListItem
                {
                    Value = tipo.IdTipousuario.ToString(),
                    Text = tipo.Descricao
                }).ToList();

            //model.TiposUsuario = tiposUsuario;
            if (id.HasValue)
            {
                var entity = _repository.get(id.Value);
                model = _mapper.Map<UsuarioModel>(entity);
            }
            model.TiposUsuario = tiposUsuario;
            return View(model);
        }

        public override IActionResult Salvar(UsuarioModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usuario = _mapper.Map<Usuario>(model);

                    usuario.IdTipousuario = model.IdTipousuario;

                    if (GetId(usuario) == 0)
                    {
                        _repository.add(usuario);
                    }
                    else
                    {
                        _repository.edit(usuario);
                    }
                    ViewBag.mensagem = "Salvo com sucesso!";
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    ViewBag.mensagem = "Erro ao salvar. Verifique os campos e tente novamente." + string.Join("<br>", errors);
                }
            }
            catch (Exception ex)
            {

                ViewBag.mensagem = "Ocorreu um erro ao salvar!" + ex.Message + " " + ex.InnerException;
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        


    }
}
