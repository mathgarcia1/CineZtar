// <copyright file="TipoUsuarioModel.cs" company="CineZtarCompany">
// Copyright (c) CineZtarCompany. All rights reserved.
// </copyright>
/// <summary>
/// Description of the class or file.
/// </summary>
/// <author>mathgarcia1</author>
/// <created>2023-05-31 14:03:46</created>
/// <lastModified>2023-05-31 14:03:46</lastModified>
namespace Cine.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Repositorio.Models;
    using Repositorio.Repositorios;

    public class TipoUsuarioModel
    {
        [KeyAttribute]
        [Required(ErrorMessage = "Código é obrigatório")]
        [Display(Name = "Código")]
        public int IdTipousuario { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatória")]
        [Display(Name = "Descrição", Prompt = "Descrição")]
        [StringLength(maximumLength: 50, ErrorMessage = "Máximo 50 Caracteres")]
        public string Descricao { get; set; }

        public TipoUsuarioModel Salvar(TipoUsuarioModel model)
        {
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            TipoUsuario tipoUsuario = mapper.Map<TipoUsuario>(model);

            using (DB_Ingressos2Context contexto = new ())
            {
                TipoUsuarioRepositorio repositorio = new (contexto);

                if (model.IdTipousuario == 0)
                {
                    repositorio.Inserir(tipoUsuario);
                }
                else
                {
                    repositorio.Alterar(tipoUsuario);
                }

                contexto.SaveChanges();
            }

            model.IdTipousuario = tipoUsuario.IdTipousuario;
            return model;
        }

        public List<TipoUsuarioModel> Listar()
        {
            List<TipoUsuarioModel> listamodel = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (DB_Ingressos2Context contexto = new ())
            {
                TipoUsuarioRepositorio repositorio = new (contexto);
                List<TipoUsuario> lista = repositorio.ListarTodos();
                listamodel = mapper.Map<List<TipoUsuarioModel>>(lista);
            }

            return listamodel;
        }

        public TipoUsuarioModel Selecionar(int id)
        {
            TipoUsuarioModel model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (DB_Ingressos2Context contexto = new ())
            {
                TipoUsuarioRepositorio repositorio = new (contexto);
                TipoUsuario tipo = repositorio.Recuperar(c => c.IdTipousuario == id);
                model = mapper.Map<TipoUsuarioModel>(tipo);
            }

            return model;
        }

        public void Excluir(int id)
        {
            using DB_Ingressos2Context contexto = new ();
            TipoUsuarioRepositorio repositorio = new (contexto);
            TipoUsuario tipoUsuario = repositorio.Recuperar(c => c.IdTipousuario == id);
            repositorio.Excluir(tipoUsuario);
            contexto.SaveChanges();
        }
    }
}
