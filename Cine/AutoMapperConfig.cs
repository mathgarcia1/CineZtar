/// <summary>
/// Description of the class or file.
/// </summary>
/// <author>mathgarcia1</author>
/// <created>2023-05-31 13:00:34</created>
/// <lastModified>2023-05-31 13:00:34</lastModified>
/// <copyright>
/// Copyright (c) 2023 mathgarcia1
/// </copyright>
namespace Cine
{
    using AutoMapper;
    using Cine.Models;
    using Repositorio.Models;

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
