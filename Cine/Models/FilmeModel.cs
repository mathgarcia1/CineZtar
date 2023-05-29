using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositorio.Models;
using Repositorio.Repositorios;

namespace Cine.Models
{
    public class FilmeModel
    {
        
        

        
        
        [KeyAttribute]
        [Required(ErrorMessage = "Código é obrigatório!")]
        [Display(Name = "Código")]
        public int IdFilme { get; set; }

        [Required(ErrorMessage = "Nome do filme é obrigatório!")]
        [Display(Name = "Nome", Prompt = "Nome do Filme")]
        [StringLength(maximumLength:50, ErrorMessage = "Máximo 50 Caractéres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Descrição do filme é obrigatório!")]
        [Display(Name = "Descrição", Prompt = "Descrição")]
        [StringLength(maximumLength:50, ErrorMessage = "Máximo 50 Caractéres")]
        public string Descricao { get; set; }
        
        [Required(ErrorMessage = "Duração do filme é obrigatório!")]
        [Display(Name = "Duração", Prompt = "Duração")]
        public int? Duracao { get; set; }

       
        [Display(Name = "Ano de lançamento", Prompt = "Ano de lançamento")]
        [StringLength(maximumLength:50, ErrorMessage = "Máximo 50 Caractéres")]
        public string AnoLancamento { get; set; }
        public string Imagem { get; set; }
        [Display(Name = "Idioma", Prompt = "Selecione um idioma")]
        public int? IdIdioma { get; set; }
        [Display(Name = "Valor", Prompt = "Valor")]
        public decimal? Valor { get; set; }
        [Display(Name = "Gênero", Prompt = "Selecione um Gênero")]
        public int? IdGenero { get; set; }
        

        //public List<SelectListItem> IdiomasFilme;

        [Display(Name = "Imagem")]
        public IFormFile ImagemUpload{get; set;}

        public FilmeModel salvar(FilmeModel model, IWebHostEnvironment webHostEnvironment){
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            Filme filme = mapper.Map<Filme>(model);

            filme.Imagem = Upload(model.ImagemUpload, webHostEnvironment);

            using (DB_Ingressos2Context contexto = new DB_Ingressos2Context())
            {
                FilmeRepositorio repositorio = new FilmeRepositorio(contexto);

                if (model.IdFilme == 0)
                    repositorio.Inserir(filme);
                else
                    repositorio.Alterar(filme);

                contexto.SaveChanges();
            }
            model.IdFilme = filme.IdFilme;
            return model;


        }


        public List<FilmeModel> listar()
        {
            List<FilmeModel> listamodel = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (DB_Ingressos2Context contexto = new DB_Ingressos2Context())
            {
                FilmeRepositorio repositorio = new FilmeRepositorio(contexto);
                List<Filme> lista = repositorio.ListarTodos();
                listamodel = mapper.Map<List<FilmeModel>>(lista);
            }

            return listamodel;
        }

        public FilmeModel selecionar(int id)
        {
            FilmeModel model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (DB_Ingressos2Context contexto = new DB_Ingressos2Context())
            {
                FilmeRepositorio repositorio = new FilmeRepositorio(contexto);
                Filme filme = repositorio.Recuperar(c => c.IdFilme == id);
                model = mapper.Map<FilmeModel>(filme);
            }
            return model;
        }

        public void excluir(int id)
        {

            using (DB_Ingressos2Context contexto = new DB_Ingressos2Context())
            {
                FilmeRepositorio repositorio = new FilmeRepositorio(contexto);
                Filme filme = repositorio.Recuperar(c => c.IdFilme == id);
                repositorio.Excluir(filme);
                contexto.SaveChanges();
            }
        }

        private string Upload(IFormFile arquivoImagem, IWebHostEnvironment webHostEnvironment)
        {
            string nomeUnicoArquivo = null;
            if (arquivoImagem != null)
            {
                string pastaFotos = Path.Combine(webHostEnvironment.WebRootPath, "Imagens"); 
                nomeUnicoArquivo = Guid.NewGuid().ToString() + "_" + arquivoImagem.FileName; 
                string caminhoArquivo = Path.Combine(pastaFotos, nomeUnicoArquivo); 
                using (var fileStream = new FileStream(caminhoArquivo, FileMode.Create))
                {
                    arquivoImagem.CopyTo(fileStream);
                }
            }
            return nomeUnicoArquivo;
        }
    }
}
