using System;
using System.Collections.Generic;
using AutoMapper;
using Cine.Models;
using Microsoft.AspNetCore.Mvc;
using Repositorio.Models;
using Repositorio.Repositorios;

namespace Cine.Controllers
{
    public class GeneroController : Controller
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
            return View(new GeneroModel());
        }
        [HttpPost]
        public IActionResult salvar(GeneroModel model) {
            if (ModelState.IsValid)
            {
                try
                {
                    GeneroModel genero = new GeneroModel();
                    genero.salvar(model);
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
            GeneroModel genero = new GeneroModel();
            List<GeneroModel> lista = genero.listar();
            return View(lista);//lista por parametro para a view
        }


        public IActionResult prealterar(int id) {
            GeneroModel model = new GeneroModel();
            return View("cadastro", model.selecionar(id));
        }

        public IActionResult excluir(int id) {
            GeneroModel model = new GeneroModel();
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
