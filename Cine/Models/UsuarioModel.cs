/// <summary>
/// Description of the class or file.
/// </summary>
/// <author>mathgarcia1</author>
/// <created>2023-05-31 14:04:52</created>
/// <lastModified>2023-05-31 14:04:52</lastModified>
/// <copyright>
/// Copyright (c) 2023 mathgarcia1
/// </copyright>
namespace Cine.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Repositorio.Models;
    using Repositorio.Repositorios;

    public class UsuarioModel
    {
        [KeyAttribute]
        [Required(ErrorMessage = "Código é obrigatório")]
        [Display(Name = "Código")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [Display(Name = "Email", Prompt = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório")]
        [StringLength(maximumLength: 50, ErrorMessage = "Máximo 250 Caracteres")]
        [Display(Name = "Senha", Prompt = "Senha")]
        public string Senha { get; set; }

        [Display(Name = "Nome", Prompt = "Nome")]
        [StringLength(maximumLength: 50, ErrorMessage = "Máximo 50 Caracteres")]
        public string Nome { get; set; }

        [Display(Name = "Tipo de Usuario", Prompt = "Selecione Tipo de Usuario")]
        public int IdTipousuario { get; set; }

        //public List<SelectListItem> TiposUsuario { get; set; }
        public UsuarioModel ValidarLogin(String email, String senha)
        {
            UsuarioModel model = null;
            using (DB_Ingressos2Context contexto = new ())
            {
                UsuarioRepositorio repositorio = new (contexto);
                Usuario usu = repositorio.Recuperar(u => u.Email == email && u.Senha == senha);
                var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
                model = mapper.Map<UsuarioModel>(usu);
            }

            return model;
        }

        public UsuarioModel Salvar(UsuarioModel model)
        {
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            Usuario usuario = mapper.Map<Usuario>(model);

            using (DB_Ingressos2Context contexto = new ())
            {
                UsuarioRepositorio repositorio = new (contexto);

                if (model.IdUsuario == 0)
                {
                    repositorio.Inserir(usuario);
                }
                else
                {
                    repositorio.Alterar(usuario);
                }

                contexto.SaveChanges();
            }

            model.IdUsuario = usuario.IdUsuario;
            return model;
        }

        public List<UsuarioModel> Listar()
        {
            List<UsuarioModel> listamodel = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (DB_Ingressos2Context contexto = new ())
            {
                UsuarioRepositorio repositorio = new (contexto);
                List<Usuario> lista = repositorio.ListarTodos();
                listamodel = mapper.Map<List<UsuarioModel>>(lista);
            }

            return listamodel;
        }

        public UsuarioModel Selecionar(int id)
        {
            UsuarioModel model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (DB_Ingressos2Context contexto = new ())
            {
                UsuarioRepositorio repositorio = new (contexto);
                Usuario usuario = repositorio.Recuperar(c => c.IdUsuario == id);
                model = mapper.Map<UsuarioModel>(usuario);
            }

            return model;
        }

        public void Excluir(int id)
        {
            using DB_Ingressos2Context contexto = new ();
            UsuarioRepositorio repositorio = new (contexto);
            Usuario usuario = repositorio.Recuperar(c => c.IdUsuario == id);
            repositorio.Excluir(usuario);
            contexto.SaveChanges();
        }
    }
}
