using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repositorio.Models;
using Repositorio.Repositorios;

namespace Cine.Models
{
    public class IngressoModel
    {

        [KeyAttribute]
        [Required(ErrorMessage = "Código é obrigatório")]
        [Display(Name = "Código")]
        public int IdIngresso { get; set; }

        [Display(Name = "Número")]
        public int? Numero { get; set; }


        [Display(Name = "Filme")]
        public int? IdFilme { get; set; }
        public decimal? Valor { get; set; }


        public List<SelectListItem> Filmes { get; set; }
        public Filme Filme { get; set; }
        private readonly IMapper _mapper;
        public IngressoModel(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IngressoModel()
        {
        }

        public List<IngressoModel> listar()
        {
            List<IngressoModel> listamodel = null;
            using (DB_IngressosContext contexto = new DB_IngressosContext())
            {
                List<Ingresso> lista = contexto.Ingressos.ToList();
                listamodel = _mapper.Map<List<IngressoModel>>(lista);
            }

            return listamodel;
        }



        public IngressoModel selecionar(int IdIngresso)
        {
            IngressoModel model = null;
            using (DB_IngressosContext contexto = new DB_IngressosContext())
            {
                IngressoRepositorio repositorio = new IngressoRepositorio();
                repositorio.SetContext(contexto); // Configurar o contexto antes de usar o repositório
                Ingresso ingresso = repositorio.Recuperar(c => c.IdIngresso == IdIngresso);
                model = _mapper.Map<IngressoModel>(ingresso);
            }
            return model;
        }

        public void excluir(int IdIngresso)
        {

            using (DB_IngressosContext contexto = new DB_IngressosContext())
            {
                IngressoRepositorio repositorio = new IngressoRepositorio(contexto);
                Ingresso ingresso = repositorio.Recuperar(i => i.IdIngresso == IdIngresso);
                repositorio.delete(ingresso);
                contexto.SaveChanges();
            }
        }
    }
}
