/// <summary>
/// Description of the class or file.
/// </summary>
/// <author>mathgarcia1</author>
/// <created>2023-05-31 14:01:35</created>
/// <lastModified>2023-05-31 14:01:35</lastModified>
/// <copyright>
/// Copyright (c) 2023 mathgarcia1
/// </copyright>
namespace Cine.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Repositorio.Models;
    using Repositorio.Repositorios;

    public class IdiomaModel
    {
        [KeyAttribute]
        [Required(ErrorMessage = "Código é obrigatório!")]
        [Display(Name = "Código")]
        public int IdIdioma { get; set; }

        [Required(ErrorMessage = "Nome do idioma é obrigatório!")]
        [Display(Name = "Idioma", Prompt = "Idioma")]
        [StringLength(maximumLength: 50, ErrorMessage = "Máximo 50 Caractéres")]
        public string Nome { get; set; }

        public IdiomaModel Salvar(IdiomaModel model)
        {
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            Idioma idioma = mapper.Map<Idioma>(model);

            using (DB_Ingressos2Context contexto = new ())
            {
                IdiomaRepositorio repositorio = new (contexto);

                if (model.IdIdioma == 0)
                {
                    repositorio.Inserir(idioma);
                }
                else
                {
                    repositorio.Alterar(idioma);
                }

                contexto.SaveChanges();
            }

            model.IdIdioma = idioma.IdIdioma;
            return model;
        }

        public List<IdiomaModel> Listar()
        {
            List<IdiomaModel> listamodel = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (DB_Ingressos2Context contexto = new ())
            {
                IdiomaRepositorio repositorio = new (contexto);
                List<Idioma> lista = repositorio.ListarTodos();
                listamodel = mapper.Map<List<IdiomaModel>>(lista);
            }

            return listamodel;
        }

        public IdiomaModel Selecionar(int id)
        {
            IdiomaModel model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (DB_Ingressos2Context contexto = new ())
            {
                IdiomaRepositorio repositorio = new (contexto);
                Idioma idioma = repositorio.Recuperar(c => c.IdIdioma == id);
                model = mapper.Map<IdiomaModel>(idioma);
            }

            return model;
        }

        public void Excluir(int id)
        {
            using DB_Ingressos2Context contexto = new ();
            IdiomaRepositorio repositorio = new (contexto);
            Idioma idioma = repositorio.Recuperar(c => c.IdIdioma == id);
            repositorio.Excluir(idioma);
            contexto.SaveChanges();
        }
    }
}
