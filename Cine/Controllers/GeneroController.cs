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
                ViewBag.mensagem = "Gênero salvo com sucesso!";
                ViewBag.classe = "alert alert-success";
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

                    ViewBag.mensagem = "Erro ao salvar gênero! " + ex.Message + "/" + ex.InnerException;
                    ViewBag.classe = "alert alert-danger";
                    return View("cadastro", model);
                }
            }
            else
            {
                ViewBag.mensagem = "Erro ao salvar gênero! verifique os campos";
                ViewBag.classe = "alert alert-danger";
                return View("cadastro", model);
            }

           
        }


        public IActionResult listar()
        {
            GeneroModel genero = new GeneroModel();
            List<GeneroModel> lista = genero.listar();
            return View(lista);
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
                ViewBag.mensagem = "Gênero excluído com sucesso!";
                ViewBag.classe = "alert alert-success";
            }
            catch (Exception ex)
            {

                ViewBag.mensagem = "Não foi possível excluir gênero! " + ex.Message;
                ViewBag.classe = "alert alert-danger";
            }

            return View("listar", model.listar());
        }
    }
}
