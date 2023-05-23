using AutoMapper;
using Cine.Models;
using Microsoft.AspNetCore.Mvc;
using Repositorio.Models;
using Repositorio.Repositorios;
using System;
using System.Collections.Generic;

namespace Cine.Controllers
{
    public class TipoUsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult cadastro(int? mostraMensagem) {
            if (mostraMensagem.HasValue)
            {
                ViewBag.mensagem = "Dados salvos com sucesso!";
                ViewBag.classe = "alert-success";
            }
            return View(new TipoUsuarioModel());
        }
        [HttpPost]
        public IActionResult salvar(TipoUsuarioModel model) {
            if (ModelState.IsValid)
            {
                try
                {
                    TipoUsuarioModel tipoUsuario = new TipoUsuarioModel();
                    tipoUsuario.salvar(model);
                    return RedirectToAction("cadastro", new { mostraMensagem = 1 });
                }
                catch (Exception ex)
                {

                    ViewBag.mensagem = "ops... Erro ao salvar!" + ex.Message + "/" + ex.InnerException;
                    ViewBag.classe = "alert-danger";
                    return View("cadastro", model);
                }
            }
            else
            {
                ViewBag.mensagem = "ops... Erro ao salvar! verifique os campos";
                ViewBag.classe = "alert-danger";
                return View("cadastro", model);
            }

           
        }


        public IActionResult listar()
        {
            TipoUsuarioModel tipoUsuario = new TipoUsuarioModel();
            List<TipoUsuarioModel> lista = tipoUsuario.listar();
            return View(lista);//lista por parametro para a view
        }


        public IActionResult prealterar(int id) {
            TipoUsuarioModel model = new TipoUsuarioModel();
            return View("cadastro", model.selecionar(id));
        }

        public IActionResult excluir(int id) {
            TipoUsuarioModel model = new TipoUsuarioModel();
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
