/// <summary>
/// Description of the class or file.
/// </summary>
/// <author>mathgarcia1</author>
/// <created>2023-05-31 13:52:16</created>
/// <lastModified>2023-05-31 13:52:16</lastModified>
/// <copyright>
/// Copyright (c) 2023 mathgarcia1
/// </copyright>
namespace Cine.Models
{
    using System.Collections.Generic;
    using AutoMapper;
    using Repositorio.Models;
    using Repositorio.Repositorios;

    public class CompraFilmeModel
    {
        public int IdCompraFilme { get; set; }

        public int? Quantidade { get; set; }

        public decimal? Valor { get; set; }

        public int? IdCompra { get; set; }

        public int? IdFilme { get; set; }

        public virtual FilmeModel Filme { get; set; }

        public CompraFilmeModel Selecionar(int id)
        {
            CompraFilmeModel model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (DB_Ingressos2Context contexto = new ())
            {
                CompraFilmeRepositorio repositorio = new (contexto);
                CompraFilme compraFilme = repositorio.Recuperar(c => c.IdCompraFilme == id);
                model = mapper.Map<CompraFilmeModel>(compraFilme);
            }

            return model;
        }

        public void Excluir(int id)
        {
            using DB_Ingressos2Context contexto = new ();
            CompraFilmeRepositorio repositorio = new (contexto);
            CompraFilme compraFilme = repositorio.Recuperar(c => c.IdCompraFilme == id);
            repositorio.Excluir(compraFilme);
            contexto.SaveChanges();
        }

        public CompraFilmeModel Salvar(CompraFilmeModel model)
        {
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            CompraFilme compraFilme = mapper.Map<CompraFilme>(model);

            using (DB_Ingressos2Context contexto = new ())
            {
                CompraFilmeRepositorio repositorio = new (contexto);

                if (model.IdCompraFilme == 0)
                {
                    repositorio.Inserir(compraFilme);
                }
                else
                {
                    repositorio.Alterar(compraFilme);
                }

                contexto.SaveChanges();
            }

            model.IdCompraFilme = compraFilme.IdCompraFilme;
            return model;
        }

        // public CompraFilmeModel salvar(CompraFilmeModel model)
        // {
        //     var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
        //     CompraFilme compraFilme = mapper.Map<CompraFilme>(model);

        // using (DB_Ingressos2Context contexto = new DB_Ingressos2Context())
        //     {
        //         CompraFilmeRepositorio repositorio = new CompraFilmeRepositorio(contexto);

        // if (model.IdCompraFilme == 0)
        //             repositorio.Inserir(compraFilme);
        //         else
        //         {
        //             var compraFilmeExistente = repositorio.Recuperar(c => c.IdCompraFilme == model.IdCompraFilme);
        //             if (compraFilmeExistente != null)
        //             {
        //                 compraFilmeExistente.Quantidade = compraFilme.Quantidade;
        //                 compraFilmeExistente.Valor = compraFilme.Valor;
        //                 compraFilmeExistente.IdCompra = compraFilme.IdCompra;
        //                 compraFilmeExistente.IdFilme = compraFilme.IdFilme;
        //                 repositorio.Alterar(compraFilmeExistente);
        //             }
        //             else
        //             {
        //                 repositorio.Inserir(compraFilme);
        //             }
        //         }

        // contexto.SaveChanges();
        //     }

        // model.IdCompraFilme = compraFilme.IdCompraFilme;
        //     return model;
        // }
        public List<CompraFilmeModel> Listar(int idcompras)
        {
            List<CompraFilmeModel> listamodel = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (DB_Ingressos2Context contexto = new ())
            {
                CompraFilmeRepositorio repositorio = new (contexto);
                List<CompraFilme> lista = repositorio.Listar(c => c.IdCompra == idcompras);
                listamodel = mapper.Map<List<CompraFilmeModel>>(lista);
                foreach (var item in listamodel)
                {
                    item.Filme = new FilmeModel().Selecionar((int)item.IdFilme);
                }
            }

            return listamodel;
        }

        public void RemoverFilme(int id, int IdFilme)
        {
            using DB_Ingressos2Context contexto = new ();
            CompraFilmeRepositorio repositorio = new (contexto);
            CompraFilme compraFilme = repositorio.Recuperar(c => c.IdCompraFilme == id);
            if (compraFilme != null)
            {
                compraFilme.IdFilme = 16;
                compraFilme.Valor = 0;
                compraFilme.Quantidade = 0;
                repositorio.Alterar(compraFilme);
                contexto.SaveChanges();
            }
        }
    }
}
