/// <summary>
/// Description of the class or file.
/// </summary>
/// <author>mathgarcia1</author>
/// <created>2023-05-31 13:45:39</created>
/// <lastModified>2023-05-31 13:45:39</lastModified>
/// <copyright>
/// Copyright (c) 2023 mathgarcia1
/// </copyright>
namespace Cine.Controllers
{
    using System;
    using System.Collections.Generic;
    using Cine.Models;
    using Microsoft.AspNetCore.Mvc;

    public class IdiomaController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult cadastro(int? mostraMensagem)
        {
            if (mostraMensagem.HasValue)
            {
                this.ViewBag.mensagem = "Idioma salvo com sucesso!";
                this.ViewBag.classe = "alert alert-success";
            }

            return this.View(new IdiomaModel());
        }

        [HttpPost]
        public IActionResult salvar(IdiomaModel model)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    IdiomaModel idioma = new IdiomaModel();
                    idioma.Salvar(model);
                    return this.RedirectToAction("cadastro", new { mostraMensagem = 1 });
                }
                catch (Exception ex)
                {
                    this.ViewBag.mensagem =
                        "Erro ao salvar idioma! " + ex.Message + "/" + ex.InnerException;
                    this.ViewBag.classe = "alert alert-danger";
                    return this.View("cadastro", model);
                }
            }
            else
            {
                this.ViewBag.mensagem = "Erro ao salvar idioma! verifique os campos";
                this.ViewBag.classe = "alert alert-danger";
                return this.View("cadastro", model);
            }
        }

        public IActionResult listar()
        {
            IdiomaModel idioma = new ();
            List<IdiomaModel> lista = idioma.Listar();
            return this.View(lista);
        }

        public IActionResult prealterar(int id)
        {
            IdiomaModel model = new ();
            return this.View("cadastro", model.Selecionar(id));
        }

        public IActionResult excluir(int id)
        {
            IdiomaModel model = new ();
            try
            {
                model.Excluir(id);
                this.ViewBag.mensagem = "Idioma excluído com sucesso!";
                this.ViewBag.classe = "alert alert-success";
            }
            catch (Exception ex)
            {
                this.ViewBag.mensagem = "Não foi possível excluir idioma! " + ex.Message;
                this.ViewBag.classe = "alert alert-danger";
            }

            return this.View("listar", model.Listar());
        }
    }
}
