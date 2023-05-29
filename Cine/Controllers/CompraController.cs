using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Cine.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositorio.Models;
using Repositorio.Repositorios;

namespace Cine.Controllers
{
    public class CompraController : Controller
    {
        public IActionResult Index()
        {
            List<CompraFilmeModel> lista = new List<CompraFilmeModel>();
            //List<CompraFilmeModel> lista = new CompraFilmeModel().listar(1);
            return View(lista);

        }

        public IActionResult excluirFilme(int id, int IdFilme)
        {

            var compraFilmeModel = new CompraFilmeModel().selecionar(id);
            // try
            // {
            //     compraFilmeModel.IdFilme = null;
            //     compraFilmeModel.Quantidade = 0;
            //     compraFilmeModel.Valor = 0;

            //     ViewBag.mensagem = "Filme excluído com sucesso!";
            //     ViewBag.classe = "alert-success";
            // }
            // catch (Exception ex)
            // {
            //     ViewBag.mensagem = "Ops... Não foi possível excluir o filme! " + ex.Message;
            //     ViewBag.classe = "alert-danger";
            // }
            compraFilmeModel.IdFilme = null;
            compraFilmeModel.Quantidade = 0;
            compraFilmeModel.Valor = 0;

            ViewBag.mensagem = "Filme excluído com sucesso!";
            ViewBag.classe = "alert-success";
            var lista = compraFilmeModel.listar(HttpContext.Session.GetInt32("IdCompra").Value);
            return View("Index", lista);
        }


        public IActionResult Finalizar()
        {
            int IdCompra = HttpContext.Session.GetInt32("IdCompra").Value;
            CompraModel compras = new CompraModel().selecionar(IdCompra);
            compras.IdStatus = 3;
            var filmes = (new CompraFilmeModel()).listar(IdCompra);
            decimal total = 0;
            foreach (var item in filmes)
            {
                total = (decimal)(total + item.Valor);
            }
            compras.Valor = total;
            //compras.idcliente = HttpContext.Session.GetInt32("idCliente").Value;


            //gerar o pagamento
            /*   ClienteModel cliente = new ClienteModel().selecionar(HttpContext.Session.GetInt32("idCliente").Value);
               Task<RetornoMercadoPago> retorno = new ComprasModel().gerarPagamentoMercadoPago(new MercadoPagoModel()
               {
                   email = cliente.email,
                   cidade = cliente.cidade,
                   cep = cliente.cep,
                   estado = cliente.estado,
                   idPagamento = idCompra,
                   logradouro = cliente.logradouro,
                   nome = cliente.nome,
                   nomePlano = "Venda Ingresso Toledo",
                   numero = cliente.numero,
                   telefone = cliente.telefone,
                   valor = total
               });*/
            //como não tenho cliente aqui vou fazer fixo para testar.
            Task<RetornoMercadoPago> retorno = new CompraModel().gerarPagamentoMercadoPago(
               new MercadoPagoModel()
               {
                   email = "meuamigovet@gmail.com",
                   cidade = "Presidente Prudente",
                   cep = "19025563",
                   estado = "SP",
                   idPagamento = IdCompra,
                   logradouro = "Avenida Brasil",
                   nome = "Cliente teste",
                   nomePlano = "Venda Ingresso Toledo",
                   numero = "20",
                   telefone = "1897865695",
                   valor = total
               });
            if (retorno.Result.status == "SUCESSO")
            {
                compras.IdPreferencia = retorno.Result.IdPreferencia;
                compras.Url = retorno.Result.Url;
            }
            else
            {
                compras.IdStatus = 4;
            }

            new CompraModel().salvar(compras);
            return Redirect(retorno.Result.Url);
        }

        public virtual JsonResult alterarQtde(int id, int qtde)
        {
            CompraFilmeModel compraFilmeModel = (new CompraFilmeModel()).selecionar(id);
            compraFilmeModel.Quantidade = qtde;
            decimal valorUnitario = (decimal)new FilmeModel().selecionar((int)compraFilmeModel.IdFilme).Valor;
            compraFilmeModel.Valor = qtde * valorUnitario;
            (new CompraFilmeModel()).salvar(compraFilmeModel);
            return new JsonResult(compraFilmeModel);
        }

        public IActionResult Finalizacao()
        {
            return View();
        }

        [HttpGet]
        public IActionResult retornoMercadoPago(string collection_id, string collection_status, string payment_id, string status, string external_reference, string payment_type, string merchant_order_id,
        string preference_id, string site_id, string processing_mode, string merchant_account_id)
        {
            CompraModel compras = new CompraModel().selecionarIdPreferencia(preference_id);
            compras.IdStatus = 2;
            new CompraModel().salvar(compras);

            if (status == "approved")
            {
                return RedirectToAction("Finalizacao", "Compra");
            }
            else
            {
                return View();
            }

        }


        public IActionResult comprarFilme(int id)
        {
            var filme = (new FilmeModel()).selecionar(id);
            var valor = filme.Valor;

            if (HttpContext.Session.GetInt32("IdCompra") == null)
            {
                var compras = new CompraModel()
                {
                    Data = DateTime.Now,
                    IdStatus = 1,
                    Valor = valor
                };

                (new CompraModel()).salvar(compras);
                HttpContext.Session.SetInt32("IdCompra", compras.IdCompra);

            }
            var compraFilmeModel = new CompraFilmeModel()
            {
                IdCompra = HttpContext.Session.GetInt32("IdCompra").Value,
                IdFilme = id,
                Quantidade = 1,
                Valor = valor
            };
            (new CompraFilmeModel()).salvar(compraFilmeModel);

            var lista = (new CompraFilmeModel()).listar(
                HttpContext.Session.GetInt32("IdCompra").Value);

            return View("Index", lista);
        }
    }
}
