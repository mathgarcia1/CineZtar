// <copyright file="CompraFilmeModel.cs" company="CineZtarCompany">
// Copyright (c) CineZtarCompany. All rights reserved.
// </copyright>
/// <summary>
/// Description of the class or file.
/// </summary>
/// <author>mathgarcia1</author>
/// <created>2023-05-31 13:52:16</created>
/// <lastModified>2023-05-31 13:52:16</lastModified>
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

        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompraFilmeModel"/> class.
        /// </summary>
        public CompraFilmeModel()
        {
           this._mapper = AutoMapperConfig.RegisterMappings().CreateMapper();
        }

        public CompraFilmeModel Selecionar(int id)
        {
            using DB_Ingressos2Context contexto = new ();
            var repositorio = new CompraFilmeRepositorio(contexto);
            CompraFilme compraFilme = repositorio.Recuperar(c => c.IdCompraFilme == id);
            return this._mapper.Map<CompraFilmeModel>(compraFilme);
        }

        public void Excluir(int id)
        {
            using DB_Ingressos2Context contexto = new ();
            var repositorio = new CompraFilmeRepositorio(contexto);
            CompraFilme compraFilme = repositorio.Recuperar(c => c.IdCompraFilme == id);
            repositorio.Excluir(compraFilme);
            contexto.SaveChanges();
        }

        public CompraFilmeModel Salvar(CompraFilmeModel model)
        {
            var compraFilme = _mapper.Map<CompraFilme>(model);

            using (var contexto = new DB_Ingressos2Context())
            {
                var repositorio = new CompraFilmeRepositorio(contexto);

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
            using DB_Ingressos2Context contexto = new ();
            var repositorio = new CompraFilmeRepositorio(contexto);
            var lista = repositorio.Listar(c => c.IdCompra == idcompras);
            var listamodel = this._mapper.Map<List<CompraFilmeModel>>(lista);
            foreach (var item in listamodel)
            {
                item.Filme = new FilmeModel().Selecionar((int)item.IdFilme);
            }

            return listamodel;
        }

        public void RemoverFilme(int id, int IdFilme)
        {
            const int ID_FILME_INVALIDO = 16;
            const int VALOR_FILME_INVALIDO = 0;
            const int QUANTIDADE_FILME_INVALIDO = 0;

            using var contexto = new DB_Ingressos2Context();
            var repositorio = new CompraFilmeRepositorio(contexto);
            var compraFilme = repositorio.Recuperar(c => c.IdCompraFilme == id);
            if (compraFilme != null)
            {
                compraFilme.IdFilme = ID_FILME_INVALIDO;
                compraFilme.Valor = VALOR_FILME_INVALIDO;
                compraFilme.Quantidade = QUANTIDADE_FILME_INVALIDO;
                repositorio.Alterar(compraFilme);
                contexto.SaveChanges();
            }
        }
    }
}
