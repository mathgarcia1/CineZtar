using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repositorio.Models;
using Repositorio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cine.Models;

namespace Cine
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddScoped<IBaseRepository<TipoUsuario>, TipoUsuarioRepositorio>();
            services.AddScoped<IBaseRepository<TipoIngresso>, TipoIngressoRepositorio>();
            services.AddScoped<IBaseRepository<Genero>, GeneroRepositorio>();
            services.AddScoped<IBaseRepository<Ingresso>, IngressoRepositorio>();
            services.AddScoped<IBaseRepository<Usuario>, UsuarioRepositorio>();
            services.AddScoped<IBaseRepository<Cinema>, CinemaRepositorio>();
            services.AddScoped<IBaseRepository<Filme>, FilmeRepositorio>();
            services.AddScoped<IBaseRepository<Idioma>, IdiomaRepositorio>();
            services.AddAutoMapper(typeof(Startup));


            // Configura��o de mapeamento
            MapperConfiguration mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TipoUsuario, TipoUsuarioModel>();
                cfg.CreateMap<TipoUsuarioModel, TipoUsuario>();

                cfg.CreateMap<Genero, GeneroModel>();
                cfg.CreateMap<GeneroModel, Genero>();

                cfg.CreateMap<TipoIngresso, TipoIngressoModel>();
                cfg.CreateMap<TipoIngressoModel, TipoIngresso>();

                cfg.CreateMap<Ingresso, IngressoModel>();
                cfg.CreateMap<IngressoModel, Ingresso>();

                cfg.CreateMap<Usuario, UsuarioModel>();
                cfg.CreateMap<UsuarioModel, Usuario>();

                cfg.CreateMap<Cinema, CinemaModel>();
                cfg.CreateMap<CinemaModel, Cinema>();

                cfg.CreateMap<Filme, FilmeModel>();
                cfg.CreateMap<FilmeModel, Filme>();

                cfg.CreateMap<Idioma, IdiomaModel>();
                cfg.CreateMap<IdiomaModel, Idioma>();


            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);


            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Usuario}/{action=Index}/{id?}");
            });
        }
    }
}
