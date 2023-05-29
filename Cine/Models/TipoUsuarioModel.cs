using AutoMapper;
using Microsoft.AspNetCore.WebUtilities;
using Repositorio.Models;
using Repositorio.Repositorios;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cine.Models
{
    public class TipoUsuarioModel
    {

        [KeyAttribute]
        [Required(ErrorMessage ="Código é obrigatório")]
        [Display(Name ="Código")]
        public int IdTipousuario { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatória")]
        [Display(Name = "Descrição", Prompt = "Descrição")]
        [StringLength(maximumLength:50, ErrorMessage ="Máximo 50 Caracteres")]
        public string Descricao { get; set; }

        public TipoUsuarioModel salvar(TipoUsuarioModel model) {
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            TipoUsuario tipoUsuario = mapper.Map<TipoUsuario>(model);

            using (DB_Ingressos2Context contexto = new DB_Ingressos2Context())
            {
                TipoUsuarioRepositorio repositorio = new TipoUsuarioRepositorio(contexto);

                if (model.IdTipousuario == 0)
                    repositorio.Inserir(tipoUsuario);
                else
                    repositorio.Alterar(tipoUsuario);

                contexto.SaveChanges();
            }
            model.IdTipousuario = tipoUsuario.IdTipousuario;
            return model;
            
        
        }
        public List<TipoUsuarioModel> listar() {
            List<TipoUsuarioModel> listamodel = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (DB_Ingressos2Context contexto = new DB_Ingressos2Context())
            {
                TipoUsuarioRepositorio repositorio = new TipoUsuarioRepositorio(contexto);
                List<TipoUsuario> lista = repositorio.ListarTodos();
                listamodel = mapper.Map<List<TipoUsuarioModel>>(lista);
            }
            
            return listamodel;
        }

        public TipoUsuarioModel selecionar(int id) {
            TipoUsuarioModel model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (DB_Ingressos2Context contexto = new DB_Ingressos2Context())
            {
                TipoUsuarioRepositorio repositorio = new TipoUsuarioRepositorio(contexto);
                TipoUsuario tipo = repositorio.Recuperar(c=>c.IdTipousuario==id);
                model = mapper.Map<TipoUsuarioModel>(tipo);
            }
            return model;
        }

        public void excluir(int id) {

            using (DB_Ingressos2Context contexto = new DB_Ingressos2Context())
            {
                TipoUsuarioRepositorio repositorio = 
                    new TipoUsuarioRepositorio(contexto);
                TipoUsuario tipoUsuario = repositorio.Recuperar(c => c.IdTipousuario == id);
                repositorio.Excluir(tipoUsuario);
                contexto.SaveChanges();
            }
        }
        


    }
}
