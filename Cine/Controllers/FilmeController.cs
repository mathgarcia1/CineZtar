using AutoMapper;
using Cine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositorio.Models;
using Repositorio.Repositorios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Cine.Controllers
{
    public class FilmeController : BaseController<Filme, FilmeModel>
    {
        private readonly IBaseRepository<Idioma> _idiomaRepository;
        public FilmeController(IBaseRepository<Filme> repository, IBaseRepository<Idioma> idiomaRepository, IMapper mapper) : base(repository, mapper)
        {
            _idiomaRepository = idiomaRepository;
        }

        protected override int GetId(Filme entity)
        {
            return entity.IdFilme;
        }

        public override IActionResult Index(int? id)
        {
            var model = new FilmeModel();

            var idiomasFilme = _idiomaRepository.getAll().
                Select(idioma => new SelectListItem
                {
                    Value = idioma.IdIdioma.ToString(),
                    Text = idioma.Nome
                }).ToList();

            if (id.HasValue)
            {
                var entity = _repository.get(id.Value);
                model = _mapper.Map<FilmeModel>(entity);

                if (entity.Imagem != null && entity.Imagem.Length > 0)
                {
                    model.Imagem = entity.Imagem;
                }
            }
            model.IdiomasFilme = idiomasFilme;
            return View(model);
        }

        public override IActionResult Salvar(FilmeModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var filme = _mapper.Map<Filme>(model);
                    if (model.ImagemUpload != null && model.ImagemUpload.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            model.ImagemUpload.CopyTo(ms);
                            filme.Imagem = ms.ToArray();
                        }
                    }
                    filme.IdIdioma = model.IdIdioma;

                    if (GetId(filme) == 0)
                    {
                        _repository.add(filme);

                    }
                    else
                    {
                        _repository.edit(filme);
                    }
                    ViewBag.Dados = "Salvo com sucesso!";
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    ViewBag.Dados = "Erro ao salvar. Verifique os campos e tente novamente." + string.Join("<br>", errors);
                }
            }
            catch (Exception ex)
            {

                ViewBag.Dados = "Ocorreu um erro ao salvar!" + ex.Message + " " + ex.InnerException;

            }
            return RedirectToAction("Index");
        }
        public IActionResult Imagem(int id)
        {
            var filme = _repository.get(id);

            if (filme != null && filme.Imagem != null)
            {
                return File(filme.Imagem, "image/jpeg");
            }

            return NotFound();
        }

    }
}