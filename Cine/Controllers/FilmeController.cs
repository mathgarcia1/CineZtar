// <copyright file="FilmeController.cs" company="CineZtarCompany">
// Copyright (c) CineZtarCompany. All rights reserved.
// </copyright>
/// <summary>
/// Description of the class or file.
/// </summary>
/// <author>mathgarcia1</author>
/// <created>2023-05-31 13:25:22</created>
/// <lastModified>2023-05-31 13:25:22</lastModified>
namespace Cine.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cine.Models;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class FilmeController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public FilmeController(IWebHostEnvironment hostEnvironment)
        {
            this.webHostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult cadastro()
        {
            List<GeneroModel> lista = new GeneroModel().Listar();
            this.ViewBag.listageneros = lista.Select(c => new SelectListItem() { Value = c.IdGenero.ToString(), Text = c.Nome });
            List<IdiomaModel> listaidioma = new IdiomaModel().Listar();
            this.ViewBag.listaidiomas = listaidioma.Select(c => new SelectListItem() { Value = c.IdIdioma.ToString(), Text = c.Nome });
            return this.View(new FilmeModel());
        }

        [HttpPost]
        public IActionResult salvar(FilmeModel model)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    FilmeModel filmemodel = new ();
                    filmemodel.Salvar(model, this.webHostEnvironment);
                    this.ViewBag.mensagem = "Filme salvo com sucesso!";
                    this.ViewBag.classe = "alert alert-success";
                }
                catch (Exception ex)
                {
                    this.ViewBag.mensagem =
                        "Erro ao salvar filme!" + ex.Message + "/" + ex.InnerException;
                    this.ViewBag.classe = "alert alert-danger";
                }
            }
            else
            {
                this.ViewBag.mensagem = "Erro ao salvar filme! verifique os campos";
                this.ViewBag.classe = "alert alert-danger";
            }

            List<GeneroModel> lista = new GeneroModel().Listar();
            this.ViewBag.listageneros = lista.Select(c => new SelectListItem() { Value = c.IdGenero.ToString(), Text = c.Nome });
            List<IdiomaModel> listaidioma = new IdiomaModel().Listar();
            this.ViewBag.listaidiomas = listaidioma.Select(c => new SelectListItem() { Value = c.IdIdioma.ToString(), Text = c.Nome });
            return this.View("cadastro", model);
        }

        public IActionResult listar()
        {
            FilmeModel filmemodel = new ();
            List<FilmeModel> lista = filmemodel.Listar();
            return this.View(lista);
        }

        public IActionResult prealterar(int id)
        {
            FilmeModel model = new ();
            List<GeneroModel> lista = new GeneroModel().Listar();
            this.ViewBag.listageneros = lista.Select(c => new SelectListItem() { Value = c.IdGenero.ToString(), Text = c.Nome });
            List<IdiomaModel> listaidioma = new IdiomaModel().Listar();
            this.ViewBag.listaidiomas = listaidioma.Select(c => new SelectListItem() { Value = c.IdIdioma.ToString(), Text = c.Nome });
            return this.View("cadastro", model.Selecionar(id));
        }

        public IActionResult excluir(int id)
        {
            FilmeModel model = new ();
            try
            {
                model.Excluir(id);
                this.ViewBag.mensagem = "Filme excluído com sucesso!";
                this.ViewBag.classe = "alert alert-success";
            }
            catch (Exception ex)
            {
                this.ViewBag.mensagem = "Não foi possível excluir filme!" + ex.Message;
                this.ViewBag.classe = "alert alert-danger";
            }

            return this.View("listar", model.Listar());
        }
    }
}
