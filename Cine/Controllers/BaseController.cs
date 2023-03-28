using AutoMapper;
using Cine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Migrations;
using Repositorio.Models;
using Repositorio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cine.Controllers
{
    public abstract class BaseController<TEntity, TModel> : Controller
        where TEntity : class
        where TModel : class
    {
        protected readonly IMapper _mapper;
        protected readonly IBaseRepository<TEntity> _repository;
        protected abstract int GetId(TEntity entity);
        protected BaseController(IBaseRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        protected BaseController(IBaseRepository<Genero> repository, IMapper mapper)
        {
        }

        public virtual IActionResult Index(int? id)
        {
            ViewBag.Dados = "LULULULU";
            TModel model = null;
            if (id.HasValue)
            {
                TEntity entity = _repository.get(id.Value);
                model = _mapper.Map<TModel>(entity);
            }
            else
            {
                model = Activator.CreateInstance<TModel>();

            }
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Salvar(TModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TEntity entity = _mapper.Map<TEntity>(model);
                    if (GetId(entity) == 0)
                    {
                        _repository.add(entity);
                    }
                    else
                    {
                        _repository.edit(entity);
                    }
                    ViewBag.mensagem = "Salvo com sucesso!";
                }

            }
            catch (Exception ex)
            {

                ViewBag.mensagem = "Ocorreu um erro ao salvar!" + ex.Message + " " + ex.InnerException;
            }
            return View("Index");
        }

        public virtual IActionResult Listar()
        {

            IEnumerable<TEntity> lista = _repository.getAll();
            List<TModel> listaModel = _mapper.Map<List<TModel>>(lista);
            return View(listaModel);
        }

        public virtual IActionResult Excluir(int id)
        {
            try
            {
                TEntity entity = _repository.get(id);
                if(entity != null)
                {
                    _repository.delete(entity);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("Listar");
        }
       


    }
}
