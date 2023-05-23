using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Repositorio.Models;
using Repositorio.Repositorios;

namespace Cine.Models
{
    public class CompraFilmeModel
    {
        
        public int IdCompraFilme { get; set; }
        public int? Quantidade { get; set; }
        public decimal? Valor { get; set; }
        public int? IdCompra { get; set; }
        public int? IdFilme { get; set; }

        public CompraFilmeModel selecionar(int id)
        {
            CompraFilmeModel model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (DB_Ingressos2Context contexto = new DB_Ingressos2Context())
            {
                CompraFilmeRepositorio repositorio = new CompraFilmeRepositorio(contexto);
                CompraFilme compraFilme = repositorio.Recuperar(c => c.IdCompraFilme == id);
                model = mapper.Map<CompraFilmeModel>(compraFilme);
            }
            return model;
        }

        public virtual FilmeModel filme { get; set; }

        public void excluir(int id){

            using (DB_Ingressos2Context contexto = new DB_Ingressos2Context())
            {
                CompraFilmeRepositorio repositorio = new CompraFilmeRepositorio(contexto);
                CompraFilme compraFilme = repositorio.Recuperar(c => c.IdCompraFilme == id);
                repositorio.Excluir(compraFilme);
                contexto.SaveChanges();
            }
        }

        public CompraFilmeModel salvar(CompraFilmeModel model){

            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            CompraFilme compraFilme = mapper.Map<CompraFilme>(model);

            using (DB_Ingressos2Context contexto = new DB_Ingressos2Context())
            {
                CompraFilmeRepositorio repositorio = new CompraFilmeRepositorio(contexto);

                if (model.IdCompraFilme == 0)
                    repositorio.Inserir(compraFilme);
                else
                    repositorio.Alterar(compraFilme);

                contexto.SaveChanges();
            }
            model.IdCompraFilme = compraFilme.IdCompraFilme;
            return model;


        }


        public List<CompraFilmeModel> listar(int idcompras){
            List<CompraFilmeModel> listamodel = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (DB_Ingressos2Context contexto = new DB_Ingressos2Context())
            {
                CompraFilmeRepositorio repositorio =
                    new CompraFilmeRepositorio(contexto);
                List<CompraFilme> lista = repositorio.Listar(c=>c.IdCompra==idcompras);
                listamodel = mapper.Map<List<CompraFilmeModel>>(lista);
                foreach (var item in listamodel)
                {
                    item.filme = new FilmeModel().selecionar((int)item.IdFilme);
                }
            }
            return listamodel;
        }
    }
}
