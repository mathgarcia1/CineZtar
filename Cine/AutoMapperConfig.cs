using AutoMapper;
using Cine.Models;
using Repositorio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cine
{
    public class AutoMapperConfig : Profile
    {
        public static MapperConfiguration RegisterMappings()
        {
            var config = new MapperConfiguration(cfg =>
            {
                
                cfg.CreateMap<Usuario, UsuarioModel>();
                cfg.CreateMap<UsuarioModel, Usuario>();

                cfg.CreateMap<TipoUsuario, TipoUsuarioModel>();
                cfg.CreateMap<TipoUsuarioModel, TipoUsuario>();

                cfg.CreateMap<Idioma, IdiomaModel>();
                cfg.CreateMap<IdiomaModel, Idioma>();

                cfg.CreateMap<Genero, GeneroModel>();
                cfg.CreateMap<GeneroModel, Genero>();
                
                cfg.CreateMap<Genero, GeneroModel>();
                cfg.CreateMap<GeneroModel, Genero>();
                
                cfg.CreateMap<Filme, FilmeModel>();
                cfg.CreateMap<FilmeModel, Filme>();

                cfg.CreateMap<Compra, CompraModel>();
                cfg.CreateMap<CompraModel, Compra>();

                cfg.CreateMap<CompraFilme, CompraFilmeModel>();
                cfg.CreateMap<CompraFilmeModel, CompraFilme>();
                
            });

            return config;
        }
    }

}
