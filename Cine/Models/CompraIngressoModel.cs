using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Repositorio.Models;
using Repositorio.Repositorios;

namespace Cine.Models
{
    public class CompraIngressoModel
    {
        
         public int IdCompraIngresso { get; set; }
        public int? Quantidade { get; set; }
        public decimal? Valor { get; set; }
        public int? IdCompra { get; set; }
        public int? IdIngresso { get; set; }

        private readonly IMapper _mapper;
        public CompraIngressoModel selecionar(int IdCompraIngresso)
        {
            CompraIngressoModel model = null;

            CompraIngresso compraIngresso = _mapper.Map<CompraIngresso>(model);
            using (DB_IngressosContext contexto = new DB_IngressosContext())
            {
                CompraIngressoRepositorio repositorio = new CompraIngressoRepositorio(contexto);
                //select * from categoria c where c.id = id
                CompraIngresso compIngresso = repositorio.Recuperar(c => c.IdCompraIngresso == IdCompraIngresso);
            }
            return model;
        }


        public virtual IngressoModel ingresso { get; set; }


        public void excluir(int IdCompraIngresso)
        {

            using (DB_IngressosContext contexto = new DB_IngressosContext())
            {
                CompraIngressoRepositorio repositorio = new CompraIngressoRepositorio(contexto);
                CompraIngresso compraIngresso = repositorio.Recuperar(c => c.IdCompraIngresso == IdCompraIngresso);
                repositorio.delete(compraIngresso);
                contexto.SaveChanges();
            }
        }

        public CompraIngressoModel salvar(CompraIngressoModel model)
        {

            
            CompraIngresso compraIngresso = _mapper.Map<CompraIngresso>(model);

            using (DB_IngressosContext contexto = new DB_IngressosContext())
            {
                CompraIngressoRepositorio repositorio =
                new CompraIngressoRepositorio(contexto);

                if (model.IdCompraIngresso == 0)
                    repositorio.add(compraIngresso);
                else
                    repositorio.edit(compraIngresso);

                contexto.SaveChanges();
            }
            model.IdCompraIngresso = compraIngresso.IdCompraIngresso;
            return model;


        }


        public List<CompraIngressoModel> listar(int IdCompras)
        {
            List<CompraIngressoModel> listamodel = null;
            using (DB_IngressosContext contexto = new DB_IngressosContext())
            {
                CompraIngressoRepositorio repositorio = new CompraIngressoRepositorio(contexto);
                List<CompraIngresso> lista = repositorio.Listar(c=>c.IdCompra==IdCompras);
                listamodel = _mapper.Map<List<CompraIngressoModel>>(lista);
                foreach (var item in listamodel)
                {
                    item.ingresso = new IngressoModel().selecionar((int)item.IdIngresso);
                }
            }

            return listamodel;
        }      
    }
}
