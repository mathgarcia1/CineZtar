/// <summary>
/// Description of the class or file.
/// </summary>
/// <author>mathgarcia1</author>
/// <created>2023-05-31 13:47:17</created>
/// <lastModified>2023-05-31 13:47:17</lastModified>
/// <copyright>
/// Copyright (c) 2023 mathgarcia1
/// </copyright>
namespace Cine.Controllers
{
    using System;
    using System.Collections.Generic;
    using Cine.Models;
    using Microsoft.AspNetCore.Mvc;

    public class TipoUsuarioController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult cadastro(int? mostraMensagem)
        {
            if (mostraMensagem.HasValue)
            {
                this.ViewBag.mensagem = "Tipo de usuário salvo com sucesso!";
                this.ViewBag.classe = "alert alert-success";
            }

            return this.View(new TipoUsuarioModel());
        }

        [HttpPost]
        public IActionResult salvar(TipoUsuarioModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    TipoUsuarioModel tipoUsuario = new TipoUsuarioModel();
                    tipoUsuario.Salvar(model);
                    return RedirectToAction("cadastro", new { mostraMensagem = 1 });
                }
                catch (Exception ex)
                {
                    this.ViewBag.mensagem =
                        "Erro ao salvar tipo de usuário! " + ex.Message + "/" + ex.InnerException;
                    this.ViewBag.classe = "alert alert-danger";
                    return this.View("cadastro", model);
                }
            }
            else
            {
                this.ViewBag.mensagem = "Erro ao salvar tipo de usuário! verifique os campos";
                this.ViewBag.classe = "alert alert-danger";
                return this.View("cadastro", model);
            }
        }

        public IActionResult listar()
        {
            TipoUsuarioModel tipoUsuario = new TipoUsuarioModel();
            List<TipoUsuarioModel> lista = tipoUsuario.Listar();
            return this.View(lista);
        }

        public IActionResult prealterar(int id)
        {
            TipoUsuarioModel model = new TipoUsuarioModel();
            return this.View("cadastro", model.Selecionar(id));
        }

        public IActionResult excluir(int id)
        {
            TipoUsuarioModel model = new TipoUsuarioModel();
            try
            {
                model.Excluir(id);
                this.ViewBag.mensagem = "Tipo de usuário excluído com sucesso!";
                this.ViewBag.classe = "alert alert-success";
            }
            catch (Exception ex)
            {
                this.ViewBag.mensagem = "Não foi possível excluir tipo de usuário!" + ex.Message;
                this.ViewBag.classe = "alert alert-danger";
            }

            return this.View("listar", model.Listar());
        }
    }
}
