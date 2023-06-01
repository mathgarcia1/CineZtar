/// <summary>
/// Description of the class or file.
/// </summary>
/// <author>mathgarcia1</author>
/// <created>2023-05-31 13:41:52</created>
/// <lastModified>2023-05-31 13:41:52</lastModified>
/// <copyright>
/// Copyright (c) 2023 mathgarcia1
/// </copyright>
namespace Cine.Controllers
{
    using System;
    using System.Collections.Generic;
    using Cine.Models;
    using Microsoft.AspNetCore.Mvc;

    public class GeneroController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult cadastro(int? mostraMensagem)
        {
            if (mostraMensagem.HasValue)
            {
                this.ViewBag.mensagem = "Gênero salvo com sucesso!";
                this.ViewBag.classe = "alert alert-success";
            }

            return this.View(new GeneroModel());
        }

        [HttpPost]
        public IActionResult salvar(GeneroModel model)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    GeneroModel genero = new ();
                    genero.Salvar(model);
                    return this.RedirectToAction("cadastro", new { mostraMensagem = 1 });
                }
                catch (Exception ex)
                {
                    this.ViewBag.mensagem =
                        "Erro ao salvar gênero! " + ex.Message + "/" + ex.InnerException;
                    this.ViewBag.classe = "alert alert-danger";
                    return this.View("cadastro", model);
                }
            }
            else
            {
                this.ViewBag.mensagem = "Erro ao salvar gênero! verifique os campos";
                this.ViewBag.classe = "alert alert-danger";
                return this.View("cadastro", model);
            }
        }

        public IActionResult listar()
        {
            GeneroModel genero = new ();
            List<GeneroModel> lista = genero.Listar();
            return this.View(lista);
        }

        public IActionResult prealterar(int id)
        {
            GeneroModel model = new ();
            return this.View("cadastro", model.Selecionar(id));
        }

        public IActionResult excluir(int id)
        {
            GeneroModel model = new ();
            try
            {
                model.Excluir(id);
                this.ViewBag.mensagem = "Gênero excluído com sucesso!";
                this.ViewBag.classe = "alert alert-success";
            }
            catch (Exception ex)
            {
                this.ViewBag.mensagem = "Não foi possível excluir gênero! " + ex.Message;
                this.ViewBag.classe = "alert alert-danger";
            }

            return this.View("listar", model.Listar());
        }
    }
}
