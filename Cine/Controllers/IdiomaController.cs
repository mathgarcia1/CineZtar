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
                ViewBag.mensagem = "Idioma salvo com sucesso!";
                ViewBag.classe = "alert alert-success";
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

                    ViewBag.mensagem = "Erro ao salvar idioma! " + ex.Message + "/" + ex.InnerException;
                    ViewBag.classe = "alert alert-danger";
                    return View("cadastro", model);
                }
            }
            else
            {
                ViewBag.mensagem = "Erro ao salvar idioma! verifique os campos";
                ViewBag.classe = "alert alert-danger";
                return View("cadastro", model);
            }

           
        }


        public IActionResult listar()
        {
            IdiomaModel idioma = new IdiomaModel();
            List<IdiomaModel> lista = idioma.listar();
            return View(lista);
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
                ViewBag.mensagem = "Idioma excluído com sucesso!";
                ViewBag.classe = "alert alert-success";
            }
            catch (Exception ex)
            {

                ViewBag.mensagem = "Não foi possível excluir idioma! " + ex.Message;
                ViewBag.classe = "alert alert-danger";
            }

            return View("listar", model.listar());
        }
    }
}
