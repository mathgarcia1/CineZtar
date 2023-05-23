using AutoMapper;
using Cine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositorio.Models;
using Repositorio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;


namespace Cine.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult login() {

            return View();
        }

        [HttpPost]
        public IActionResult logar(String txtemail,
            String txtsenha) {
            UsuarioModel model = (new UsuarioModel()).validarLogin(txtemail, txtsenha);
            if (model == null)
            {
                //não encontrou 
                ViewBag.mensagem = "Dados inválidos";
                ViewBag.classe = "alert-danger";
                return View("login");
            }
            else {
                //encontrou
                //inseriu na sessão
                HttpContext.Session.SetInt32("IdUsuario", model.IdUsuario);
                HttpContext.Session.SetString("Nome", model.Nome);
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult sair() {
            //limpar a sessão
            HttpContext.Session.Remove("Nome");
            HttpContext.Session.Remove("IdUsuario");
            HttpContext.Session.Clear();

            //redirecionar para login
            return RedirectToAction("login", "Usuario");
        }

        [HttpPost]
        public IActionResult salvar(UsuarioModel model)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    UsuarioModel usuarioModel = new UsuarioModel();
                    usuarioModel.salvar(model);
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

            List<TipoUsuarioModel> lista = (new TipoUsuarioModel()).listar();
            ViewBag.listatipos = lista.Select(c => new SelectListItem()
            {
                Value = c.IdTipousuario.ToString(),
                Text = c.Descricao
            });

            return View("cadastro", model);
        }


        public IActionResult listar()
        {
            UsuarioModel usuarioModel = new UsuarioModel();
            List<UsuarioModel> lista = usuarioModel.listar();
            return View(lista);//lista por parametro para a view
        }


        public IActionResult prealterar(int id)
        {
            UsuarioModel usuarioModel = new UsuarioModel();
            List<TipoUsuarioModel> lista = (new TipoUsuarioModel()).listar();
            ViewBag.listatipos = lista.Select(c => new SelectListItem()
            {
                Value = c.IdTipousuario.ToString(),
                Text = c.Descricao
            });
            return View("cadastro", usuarioModel.selecionar(id));
        }

        public IActionResult excluir(int id)
        {
            UsuarioModel usuarioModel = new UsuarioModel();
            try
            {

                usuarioModel.excluir(id);
                ViewBag.mensagem = "Dados excluidos com sucesso!";
                ViewBag.classe = "alert-success";
            }
            catch (Exception ex)
            {

                ViewBag.mensagem = "Ops... Não foi possível excluir!" + ex.Message;
                ViewBag.classe = "alert-danger";
            }

            return View("listar", usuarioModel.listar());
        }
    }
}
