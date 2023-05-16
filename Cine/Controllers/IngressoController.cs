using System;
using System.Linq;
using AutoMapper;
using Cine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositorio.Models;
using Repositorio.Repositorios;

namespace Cine.Controllers
{
    public class IngressoController : BaseController<Ingresso, IngressoModel>
    {
        private readonly IBaseRepository<Filme> _filmeRepository;
        public IngressoController(IBaseRepository<Ingresso> repository, IBaseRepository<Filme> filmeRepository,IMapper mapper) : base(repository, mapper)
        {
            _filmeRepository = filmeRepository;
        }

        protected override int GetId(Ingresso entity)
        {
            return entity.IdIngresso;
        }
        public override IActionResult Index(int? id)
        {
            var model = new IngressoModel();
            var filmes = _filmeRepository.getAll().
                Select(filme => new SelectListItem
                {
                    Value = filme.IdFilme.ToString(),
                    Text = filme.Nome
                }).ToList();

            if (id.HasValue)
            {
                var entity = _repository.get(id.Value);
                model = _mapper.Map<IngressoModel>(entity);
            }
            model.Filmes = filmes;
            return View(model);
        }

        public override IActionResult Salvar(IngressoModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var ingresso = _mapper.Map<Ingresso>(model);

                    ingresso.IdFilme = model.IdFilme;

                    if (GetId(ingresso) == 0)
                    {
                        _repository.add(ingresso);
                    }
                    else
                    {
                        _repository.edit(ingresso);
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
    }
}
