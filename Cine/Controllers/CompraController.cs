// <copyright file="CompraController.cs" company="CineZtarCompany">
// Copyright (c) CineZtarCompany. All rights reserved.
// </copyright>
/// <summary>
/// Description of the class or file.
/// </summary>
/// <author>mathgarcia1</author>
/// <created>2023-05-31 13:05:12</created>
/// <lastModified>2023-05-31 13:05:12</lastModified>
namespace Cine.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Cine.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class CompraController : Controller
    {
        public IActionResult Index()
        {
            List<CompraFilmeModel> lista = new ();

            // List<CompraFilmeModel> lista = new CompraFilmeModel().listar(1);
            return this.View(lista);
        }

        public IActionResult excluirFilme(int id, int IdFilme)
        {
            var compraFilmeModel = new CompraFilmeModel().Selecionar(id);
            try
            {
                compraFilmeModel.RemoverFilme(id, IdFilme);
                this.ViewBag.mensagem = "Filme excluído com sucesso!";
                this.ViewBag.classe = "alert alert-success";
            }
            catch (Exception ex)
            {
                this.ViewBag.mensagem = "Ops... Não foi possível excluir o filme! " + ex.Message;
                this.ViewBag.classe = "alert alert-danger";
            }

            // try
            // {
            //     compraFilmeModel.IdFilme = null;
            //     compraFilmeModel.Quantidade = 0;
            //     compraFilmeModel.Valor = 0;
            //     this.ViewBag.mensagem = "Filme excluído com sucesso!";
            //     this.ViewBag.classe = "alert alert-success";
            // }
            // catch (Exception ex)
            // {
            //     this.ViewBag.mensagem = "Ops... Não foi possível excluir o filme! " + ex.Message;
            //     this.ViewBag.classe = "alert alert-danger";
            // }
            // compraFilmeModel.IdFilme = null;
            // compraFilmeModel.Quantidade = 0;
            // compraFilmeModel.Valor = 0;

            // this.ViewBag.mensagem = "Filme excluído com sucesso!";
            // this.ViewBag.classe = "alert alert-success";
            var lista = compraFilmeModel.Listar(this.HttpContext.Session.GetInt32("IdCompra").Value);
            return this.View("Index", lista);
        }

        public IActionResult Finalizar()
        {
            int IdCompra = this.HttpContext.Session.GetInt32("IdCompra").Value;
            CompraModel compras = new CompraModel().Selecionar(IdCompra);
            compras.IdStatus = 3;
            var filmes = new CompraFilmeModel().Listar(IdCompra);
            decimal total = 0;
            foreach (var item in filmes)
            {
                total = (decimal)(total + item.Valor);
            }

            compras.Valor = total;
            /*compras.idcliente = this.HttpContext.Session.GetInt32("idCliente").Value;
            gerar o pagamento
               ClienteModel cliente = new ClienteModel().selecionar(this.HttpContext.Session.GetInt32("idCliente").Value);
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
               });
            como não tenho cliente aqui vou fazer fixo para testar.*/
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
                    valor = total,
                });
            if (retorno.Result.Status == "SUCESSO")
            {
                compras.IdPreferencia = retorno.Result.IdPreferencia;
                compras.Url = retorno.Result.Url;
            }
            else
            {
                compras.IdStatus = 4;
                compras.Url = null;
            }

            new CompraModel().Salvar(compras);
            if (!string.IsNullOrEmpty(compras.Url))
            {
                return this.Redirect(retorno.Result.Url);
            }
            else
            {
                return this.RedirectToAction("Falha", "Compra");
            }
        }

        public virtual JsonResult alterarQtde(int id, int qtde)
        {
            CompraFilmeModel compraFilmeModel = new CompraFilmeModel().Selecionar(id);
            compraFilmeModel.Quantidade = qtde;
            decimal valorUnitario = (decimal)new FilmeModel().Selecionar((int)compraFilmeModel.IdFilme).Valor;
            compraFilmeModel.Valor = qtde * valorUnitario;
            new CompraFilmeModel().Salvar(compraFilmeModel);
            return new JsonResult(compraFilmeModel);
        }

        public IActionResult Finalizacao()
        {
            return this.View();
        }

        public IActionResult Falha()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult retornoMercadoPago(
            string collection_id,
            string collection_status,
            string payment_id,
            string status,
            string external_reference,
            string payment_type,
            string merchant_order_id,
            string preference_id,
            string site_id,
            string processing_mode,
            string merchant_account_id)
        {
            CompraModel compras = new CompraModel().SelecionarIdPreferencia(preference_id);
            compras.IdStatus = 2;
            new CompraModel().Salvar(compras);

            if (status == "approved")
            {
                return this.RedirectToAction("Finalizacao", "Compra");
            }
            else
            {
                return this.View();
            }
        }

        public IActionResult comprarFilme(int id)
        {
            var filme = new FilmeModel().Selecionar(id);
            var valor = filme.Valor;

            if (this.HttpContext.Session.GetInt32("IdCompra") == null)
            {
                var compras = new CompraModel()
                {
                    Data = DateTime.Now,
                    IdStatus = 1,
                    Valor = valor,
                };

                new CompraModel().Salvar(compras);
                this.HttpContext.Session.SetInt32("IdCompra", compras.IdCompra);
            }

            var compraFilmeModel = new CompraFilmeModel()
            {
                IdCompra = this.HttpContext.Session.GetInt32("IdCompra").Value,
                IdFilme = id,
                Quantidade = 1,
                Valor = valor,
            };
            new CompraFilmeModel().Salvar(compraFilmeModel);

            var lista = new CompraFilmeModel().Listar(this.HttpContext.Session.GetInt32("IdCompra").Value);
            return this.View("Index", lista);
        }
    }
}
