using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Repositorio.Models;
using Repositorio.Repositorios;
namespace Cine.Models
{
    
    public class CompraModel
    {

        public int IdCompra { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? Data { get; set; }
        public int? QtdIngressos { get; set; }
        public int? IdIngresso { get; set; }
        public int? IdStatus { get; set; }
        public decimal? Valor { get; set; }

        private readonly IMapper _mapper;
        public CompraModel salvar(CompraModel model)
        {
            Compra compra = _mapper.Map<Compra>(model);
            using (DB_IngressosContext contexto = new DB_IngressosContext())
            {
                CompraRepositorio repositorio = new CompraRepositorio(contexto);

                if (model.IdCompra == 0)
                    repositorio.add(compra);
                else
                    repositorio.edit(compra);

                contexto.SaveChanges();
            }
            model.IdCompra = compra.IdCompra;
            return model;


        }
    }
}
