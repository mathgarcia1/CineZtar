using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repositorio.Models;
using Repositorio.Repositorios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cine.Models
{
    public class UsuarioModel
    {
        [KeyAttribute]
        [Required(ErrorMessage = "Código é obrigatório")]
        [Display(Name = "Código")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório")]
        [StringLength(maximumLength: 50, ErrorMessage = "Máximo 250 Caracteres")]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [Display(Name = "Nome")]
        [StringLength(maximumLength: 50, ErrorMessage = "Máximo 50 Caracteres")]
        public string Nome { get; set; }


        [Display(Name = "Tipo de Usuario")]
        public int IdTipousuario { get; set; }



        //public List<SelectListItem> TiposUsuario { get; set; }
        public UsuarioModel validarLogin(String email, String senha)
        {
            UsuarioModel model = null;
            using (DB_Ingressos2Context contexto = new DB_Ingressos2Context())
            {
                UsuarioRepositorio repositorio = new UsuarioRepositorio(contexto);
                 Usuario usu =repositorio.Recuperar
                    (u=>u.Email==email && u.Senha==senha);
                var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
                model = mapper.Map<UsuarioModel>(usu);
                }
            return model;
        }

        public UsuarioModel salvar(UsuarioModel model) {
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            Usuario usuario = mapper.Map<Usuario>(model);

            using (DB_Ingressos2Context contexto = new DB_Ingressos2Context())
            {
                UsuarioRepositorio repositorio = new UsuarioRepositorio(contexto);

                if (model.IdTipousuario == 0)
                    repositorio.Inserir(usuario);
                else
                    repositorio.Alterar(usuario);

                contexto.SaveChanges();
            }
            model.IdUsuario = usuario.IdUsuario;
            return model;
        }

        public List<UsuarioModel> listar() {
            List<UsuarioModel> listamodel = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (DB_Ingressos2Context contexto = new DB_Ingressos2Context())
            {
                UsuarioRepositorio repositorio = new UsuarioRepositorio(contexto);
                List<Usuario> lista = repositorio.ListarTodos();
                listamodel = mapper.Map<List<UsuarioModel>>(lista);
            }
            
            return listamodel;
        }

        public UsuarioModel selecionar(int id) {
            UsuarioModel model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (DB_Ingressos2Context contexto = new DB_Ingressos2Context())
            {
                UsuarioRepositorio repositorio = new UsuarioRepositorio(contexto);
                Usuario usuario = repositorio.Recuperar(c=>c.IdUsuario==id);
                model = mapper.Map<UsuarioModel>(usuario);
            }
            return model;
        }

        public void excluir(int id) {

            using (DB_Ingressos2Context contexto = new DB_Ingressos2Context())
            {
                UsuarioRepositorio repositorio = new UsuarioRepositorio(contexto);
                Usuario usuario = repositorio.Recuperar(c => c.IdUsuario == id);
                repositorio.Excluir(usuario);
                contexto.SaveChanges();
            }
        }
    }
}
