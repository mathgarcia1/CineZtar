using System;
using System.Collections.Generic;
using AutoMapper;
using Cine.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositorio.Models;
using Repositorio.Repositorios;

namespace Cine.Controllers
{
    public class CompraController : BaseController<Compra, CompraModel>
    {
        public CompraController(IBaseRepository<Compra> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        protected override int GetId(Compra entity)
        {
            return entity.IdCompra;
        }
        public override IActionResult Index(int? id)
        {
            List<CompraIngressoModel> list = new List<CompraIngressoModel>();
            return View(list);
        }

        public IActionResult excluirIngresso(int IdIngresso) {
            (new CompraIngressoModel()).excluir(IdIngresso);
            var lista = (new CompraIngressoModel()).listar(
                HttpContext.Session.GetInt32("IdCompra").Value);

            return View("Index", lista);
        }

        public virtual JsonResult alterarQuantidade(int id, int Quantidade) {
            CompraIngressoModel ingresso = (new CompraIngressoModel()).selecionar(id);
            ingresso.Quantidade = Quantidade;
            decimal valorUnitario = (decimal)new CompraIngressoModel().selecionar((int)ingresso.IdIngresso).Valor;
            ingresso.Valor = Quantidade * valorUnitario;
            (new CompraIngressoModel()).salvar(ingresso);
            return new JsonResult(ingresso);

        }

        public IActionResult comprarIngresso(int IdIngresso) {
            var ingresso = (new IngressoModel()).selecionar(IdIngresso);
            var valor = ingresso.Valor;

            if (HttpContext.Session.GetInt32("IdCompra") == null)
            {

                var compra = new CompraModel()
                {
                    Data = DateTime.Now,
                    IdStatus = 1,
                    Valor = valor
                };

                (new CompraModel()).salvar(compra);
                HttpContext.Session.SetInt32("IdCompra", compra.IdCompra);

            }
            var compraIngresso = new CompraIngressoModel() {
                IdCompra = HttpContext.Session.GetInt32("IdCompra").Value,
                IdIngresso = IdIngresso,
                Quantidade = 1,
                Valor = valor
            };
            (new CompraIngressoModel()).salvar(compraIngresso);

            var lista = (new CompraIngressoModel()).listar(
                HttpContext.Session.GetInt32("IdCompra").Value);

            return View("Index", lista);
        }
    }
}
