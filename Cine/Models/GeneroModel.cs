using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Repositorio.Models;
using Repositorio.Repositorios;

namespace Cine.Models
{
    public class GeneroModel
    {
        [KeyAttribute]
        [Required(ErrorMessage = "Código é obrigatório!")]
        [Display(Name = "Código")]
        public int IdGenero { get; set; }

        [Required(ErrorMessage = "Nome do gênero é obrigatório!")]
        [Display(Name = "Gênero", Prompt = "Gênero")]
        [StringLength(maximumLength:50, ErrorMessage = "Máximo 50 Caractéres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Descrição do gênero é obrigatório!")]
        [Display(Name = "Descrição", Prompt = "Descrição")]
        [StringLength(maximumLength: 150, ErrorMessage = "Máximo 150 Caractéres")]
        public string Descricao { get; set; }

        public GeneroModel salvar(GeneroModel model) {

            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            Genero genero = mapper.Map<Genero>(model);

            using (DB_Ingressos2Context contexto = new DB_Ingressos2Context())
            {
                GeneroRepositorio repositorio = new GeneroRepositorio(contexto);

                if (model.IdGenero == 0)
                    repositorio.Inserir(genero);
                else
                    repositorio.Alterar(genero);

                contexto.SaveChanges();
            }
            model.IdGenero = genero.IdGenero;
            return model;
            
        
        }

        public List<GeneroModel> listar() {
            List<GeneroModel> listamodel = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (DB_Ingressos2Context contexto = new DB_Ingressos2Context())
            {
                GeneroRepositorio repositorio = new GeneroRepositorio(contexto);
                List<Genero> lista = repositorio.ListarTodos();
                listamodel = mapper.Map<List<GeneroModel>>(lista);
            }
            
            return listamodel;
        }

        public GeneroModel selecionar(int id) {
            GeneroModel model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (DB_Ingressos2Context contexto = new DB_Ingressos2Context())
            {
                GeneroRepositorio repositorio = new GeneroRepositorio(contexto);
                Genero genero = repositorio.Recuperar(c=>c.IdGenero==id);
                model = mapper.Map<GeneroModel>(genero);
            }
            return model;
        }

        public void excluir(int id) {

            using (DB_Ingressos2Context contexto = new DB_Ingressos2Context())
            {
                GeneroRepositorio repositorio = new GeneroRepositorio(contexto);
                Genero genero = repositorio.Recuperar(c => c.IdGenero == id);
                repositorio.Excluir(genero);
                contexto.SaveChanges();
            }
        }
    }
}
