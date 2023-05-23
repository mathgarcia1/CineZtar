using AutoMapper;
using Cine.Models;
using Microsoft.AspNetCore.Hosting;
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
    public class FilmeController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        public FilmeController(IWebHostEnvironment hostEnvironment)
        {
            webHostEnvironment = hostEnvironment;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult cadastro() {
            List<GeneroModel> lista = (new GeneroModel()).listar();
            ViewBag.listageneros = lista.Select(c=>new SelectListItem() { 
               Value = c.IdGenero.ToString(), Text = c.Descricao
            });
            List<IdiomaModel> listaidioma = (new IdiomaModel()).listar();
            ViewBag.listaidiomas = listaidioma.Select(c=>new SelectListItem() { 
               Value = c.IdIdioma.ToString(), Text = c.Nome
            });
            return View(new FilmeModel());
        }


       

        [HttpPost]
        public IActionResult salvar(FilmeModel model)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    FilmeModel filmemodel = new FilmeModel();
                    filmemodel.salvar(model, webHostEnvironment);
                    ViewBag.mensagem = "Dados salvos com sucesso!";
                    ViewBag.classe = "alert-success";
                }
                catch (Exception ex)
                {

                    ViewBag.mensagem = "ops... Erro ao salvar!" + ex.Message + "/" + ex.InnerException;
                    ViewBag.classe = "alert-danger";
                }
            }
            else
            {
                ViewBag.mensagem = "ops... Erro ao salvar! verifique os campos";
                ViewBag.classe = "alert-danger";

            }

            List<GeneroModel> lista = (new GeneroModel()).listar();
            ViewBag.listageneros = lista.Select(c => new SelectListItem()
            {
                Value = c.IdGenero.ToString(),
                Text = c.Descricao
            });
            List<IdiomaModel> listaidioma = (new IdiomaModel()).listar();
            ViewBag.listaidiomas = listaidioma.Select(c=>new SelectListItem() { 
                Value = c.IdIdioma.ToString(),
                Text = c.Nome
            });
            return View("cadastro", model);
        }


        public IActionResult listar()
        {
            FilmeModel filmemodel = new FilmeModel();
            List<FilmeModel> lista = filmemodel.listar();
            return View(lista);
        }


        public IActionResult prealterar(int id)
        {
            FilmeModel model = new FilmeModel();
            List<GeneroModel> lista = (new GeneroModel()).listar();
            ViewBag.listageneros = lista.Select(c => new SelectListItem()
            {
                Value = c.IdGenero.ToString(),
                Text = c.Descricao
            });
            List<IdiomaModel> listaidioma = (new IdiomaModel()).listar();
            ViewBag.listaidiomas = listaidioma.Select(c=>new SelectListItem() { 
                Value = c.IdIdioma.ToString(),
                Text = c.Nome
            });
            return View("cadastro", model.selecionar(id));
        }

        public IActionResult excluir(int id)
        {
            FilmeModel model = new FilmeModel();
            try
            {

                model.excluir(id);
                ViewBag.mensagem = "Dados excluidos com sucesso!";
                ViewBag.classe = "alert-success";
            }
            catch (Exception ex)
            {

                ViewBag.mensagem = "Ops... Não foi possível excluir!" + ex.Message;
                ViewBag.classe = "alert-danger";
            }

            return View("listar", model.listar());
        }

    }
}