using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositorio.Models;
using Repositorio.Repositorios;
//using Cine.Models;

namespace Cine.Controllers
{
    public class SalaController : BaseController<Sala, SalaModel>
    {
        private readonly IBaseRepository<Cinema> _cinemaRepository;
        public SalaController(IBaseRepository<Sala> repository, IBaseRepository<Cinema> cinemaRepository, IMapper mapper) : base(repository, mapper)
        {
            _cinemaRepository = cinemaRepository;
        }

        protected override int GetId(Sala entity)
        {
            return entity.IdSala;
        }

        public override IActionResult Index(int? id)
        {
            var model = new SalaModel();

            var cinemas = _cinemaRepository.getAll()
                .Select(cinema => new SelectListItem
                {
                    Value = cinema.IdCinema.ToString(),
                    Text = cinema.Nome
                }).ToList();

            if (id.HasValue)
            {
                var entity = _repository.get(id.Value);
                model = _mapper.Map<SalaModel>(entity);
            }
            model.Cinema = cinemas;
            return View(model);
        }

        public override IActionResult Salvar(SalaModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var sala = _mapper.Map<Sala>(model);
                    sala.IdCinema = model.IdCinema;

                    if (GetId(sala) == 0)
                    {
                        var cinema = _cinemaRepository.get(sala.IdCinema.Value);
                        cinema.TotalSalaCinema++;
                        _cinemaRepository.edit(cinema);
                        _repository.add(sala); ;
                    }
                    else
                    {
                        _repository.edit(sala);
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
            return RedirectToAction("Listar");
        }
        public override IActionResult Excluir(int id)
        {
            try
            {
                var sala = _repository.get(id);
                var cinema = _cinemaRepository.get(sala.IdCinema.Value);
                if (sala != null)
                {
                    cinema.TotalSalaCinema--;
                    _cinemaRepository.edit(cinema);
                    _repository.delete(sala);
                }
            }
            catch (Exception ex)
            {
                ViewBag.mensagem = "Ocorreu um erro ao excluir!" + ex.Message + " " + ex.InnerException;
            }
            return RedirectToAction("Listar");


        }
    }
}
