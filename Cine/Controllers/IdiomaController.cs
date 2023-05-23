using System;
using System.Collections.Generic;
using AutoMapper;
using Cine.Models;
using Microsoft.AspNetCore.Mvc;
using Repositorio.Models;
using Repositorio.Repositorios;

namespace Cine.Controllers
{
    public class IdiomaController : Controller
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
            return View(new IdiomaModel());
        }
        [HttpPost]
        public IActionResult salvar(IdiomaModel model) {
            if (ModelState.IsValid)
            {
                try
                {
                    IdiomaModel idioma = new IdiomaModel();
                    idioma.salvar(model);
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
            IdiomaModel idioma = new IdiomaModel();
            List<IdiomaModel> lista = idioma.listar();
            return View(lista);//lista por parametro para a view
        }


        public IActionResult prealterar(int id) {
            IdiomaModel model = new IdiomaModel();
            return View("cadastro", model.selecionar(id));
        }

        public IActionResult excluir(int id) {
            IdiomaModel model = new IdiomaModel();
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
