/// <summary>
/// Description of the class or file.
/// </summary>
/// <author>mathgarcia1</author>
/// <created>2023-05-31 13:49:02</created>
/// <lastModified>2023-05-31 13:49:02</lastModified>
/// <copyright>
/// Copyright (c) 2023 mathgarcia1
/// </copyright>
namespace Cine.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cine.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult login()
        {
            return this.View();
        }

        public IActionResult cadastro(int? mostraMensagem)
        {
            List<TipoUsuarioModel> lista = new TipoUsuarioModel().Listar();
            this.ViewBag.listatipos = lista.Select(
                u => new SelectListItem() { Value = u.IdTipousuario.ToString(), Text = u.Descricao });
            if (mostraMensagem.HasValue)
            {
                this.ViewBag.mensagem = "Dados salvos com sucesso!";
                this.ViewBag.classe = "alert-success";
            }

            return this.View(new UsuarioModel());
        }

        [HttpPost]
        public IActionResult logar(String Email, String Senha)
        {
            UsuarioModel model = new UsuarioModel().ValidarLogin(Email, Senha);
            if (model == null)
            {
                this.ViewBag.mensagem = "Dados inválidos";
                this.ViewBag.classe = "alert alert-danger";
                return View("login");
            }
            else
            {
                this.HttpContext.Session.SetInt32("IdUsuario", model.IdUsuario);
                this.HttpContext.Session.SetString("Nome", model.Nome);
                return this.RedirectToAction("Index", "Home");
            }
        }

        public IActionResult sair()
        {
            this.HttpContext.Session.Remove("IdUsuario");
            this.HttpContext.Session.Remove("Nome");
            this.HttpContext.Session.Clear();

            return this.RedirectToAction("login", "Usuario");
        }

        [HttpPost]
        public IActionResult salvar(UsuarioModel model)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    UsuarioModel usuarioModel = new UsuarioModel();

                    usuarioModel.Salvar(model);
                    this.ViewBag.mensagem = "Usuário cadastrado com sucesso!";
                    this.ViewBag.classe = "alert alert-success";
                }
                catch (Exception ex)
                {
                    this.ViewBag.mensagem =
                        "Erro ao cadastrar usuário! " + ex.Message + "/" + ex.InnerException;
                    this.ViewBag.classe = "alert alert-danger";
                }
            }
            else
            {
                this.ViewBag.mensagem = "Erro ao cadastrar usuário! verifique os campos";
                this.ViewBag.classe = "alert alert-danger";
            }

            List<TipoUsuarioModel> lista = new TipoUsuarioModel().Listar();
            this.ViewBag.listatipos = lista.Select(
                c => new SelectListItem() { Value = c.IdTipousuario.ToString(), Text = c.Descricao });

            return this.View("cadastro", model);
        }

        public IActionResult listar()
        {
            UsuarioModel usuarioModel = new ();
            List<UsuarioModel> lista = usuarioModel.Listar();
            return this.View(lista);
        }

        public IActionResult prealterar(int id)
        {
            UsuarioModel usuarioModel = new ();
            List<TipoUsuarioModel> lista = new TipoUsuarioModel().Listar();
            this.ViewBag.listatipos = lista.Select(
                c => new SelectListItem() { Value = c.IdTipousuario.ToString(), Text = c.Descricao });
            return this.View("cadastro", usuarioModel.Selecionar(id));
        }

        public IActionResult Excluir(int id)
        {
            UsuarioModel usuarioModel = new ();
            try
            {
                usuarioModel.Excluir(id);
                this.ViewBag.mensagem = "Usuário excluído com sucesso!";
                this.ViewBag.classe = "alert alert-success";
            }
            catch (Exception ex)
            {
                this.ViewBag.mensagem = "Não foi possível excluir usuário! " + ex.Message;
                this.ViewBag.classe = "alert alert-danger";
            }

            return this.View("listar", usuarioModel.Listar());
        }
    }
}
