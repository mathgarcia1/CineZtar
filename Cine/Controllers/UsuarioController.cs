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
        public IActionResult cadastro(int? mostraMensagem) {
            List<TipoUsuarioModel> lista = (new TipoUsuarioModel()).listar();
            ViewBag.listatipos = lista.Select(u=>new SelectListItem(){
                Value = u.IdTipousuario.ToString(),
                Text = u.Descricao
                });
            if (mostraMensagem.HasValue)
            {
                ViewBag.mensagem = "Dados salvos com sucesso!";
                ViewBag.classe = "alert-success";
            }
            return View(new UsuarioModel());
        }

        [HttpPost]
        public IActionResult logar(String Email, String Senha) 
        {
            UsuarioModel model = (new UsuarioModel()).validarLogin(Email, Senha);
            if (model == null)
            {
                ViewBag.mensagem = "Dados inválidos";
                ViewBag.classe = "alert alert-danger";
                return View("login");
            }
            else {
                HttpContext.Session.SetInt32("IdUsuario", model.IdUsuario);
                HttpContext.Session.SetString("Nome", model.Nome);
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult sair() {
            HttpContext.Session.Remove("IdUsuario");
            HttpContext.Session.Remove("Nome");
            HttpContext.Session.Clear();

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
                    ViewBag.mensagem = "Usuário cadastrado com sucesso!";
                    ViewBag.classe = "alert alert-success";
                }
                catch (Exception ex)
                {

                    ViewBag.mensagem = "Erro ao cadastrar usuário! " + ex.Message + "/" + ex.InnerException;
                    ViewBag.classe = "alert alert-danger";
                }
            }
            else
            {
                ViewBag.mensagem = "Erro ao cadastrar usuário! verifique os campos";
                ViewBag.classe = "alert alert-danger";

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
            return View(lista);
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
                ViewBag.mensagem = "Usuário excluído com sucesso!";
                ViewBag.classe = "alert alert-success";
            }
            catch (Exception ex)
            {

                ViewBag.mensagem = "Não foi possível excluir usuário! " + ex.Message;
                ViewBag.classe = "alert alert-danger";
            }

            return View("listar", usuarioModel.listar());
        }
    }
}
