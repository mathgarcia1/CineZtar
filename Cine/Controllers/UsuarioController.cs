using AutoMapper;
using Cine.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositorio.Models;
using Repositorio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;


namespace Cine.Controllers
{
    public class UsuarioController : BaseController<Usuario, UsuarioModel>
    {
        private readonly IBaseRepository<TipoUsuario> _tipoUsuarioRepository;
        //private readonly IBaseRepository<Usuario> _usuarioRepository;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _config;
        private string JwtKey { get; set; }


        public UsuarioController(IBaseRepository<Usuario> usuarioRepository, IBaseRepository<TipoUsuario> tipoUsuarioRepository, IMapper mapper, Microsoft.Extensions.Configuration.IConfiguration config)
            : base(usuarioRepository, mapper)
        {

            //_usuarioRepository = usuarioRepository;
            _tipoUsuarioRepository = tipoUsuarioRepository;
            _config = config;
            JwtKey = "JwtKey";
        }

        protected override int GetId(Usuario entity)
        {
            return entity.IdUsuario;
        }

        public override IActionResult Index(int? id)
        {
            //ViewBag.TiposUsuario = _tipoUsuarioRepository.getAll().ToList();
            //return base.Index(id);

            var model = new UsuarioModel();
            var tiposUsuario = _tipoUsuarioRepository.getAll().
                Select(tipo => new SelectListItem
                {
                    Value = tipo.IdTipousuario.ToString(),
                    Text = tipo.Descricao
                }).ToList();

            //model.TiposUsuario = tiposUsuario;
            if (id.HasValue)
            {
                var entity = _repository.get(id.Value);
                model = _mapper.Map<UsuarioModel>(entity);
            }
            model.TiposUsuario = tiposUsuario;
            return View(model);
        }

        public override IActionResult Salvar(UsuarioModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usuario = _mapper.Map<Usuario>(model);

                    usuario.IdTipousuario = model.IdTipousuario;

                    if (GetId(usuario) == 0)
                    {
                        _repository.add(usuario);
                    }
                    else
                    {
                        _repository.edit(usuario);
                    }
                    ViewBag.mensagem = "Salvo com sucesso!";
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    ViewBag.mensagem = "Erro ao salvar. Verifique os campos e tente novamente." + string.Join("<br>", errors);
                }
            }
            catch (Exception ex)
            {

                ViewBag.mensagem = "Ocorreu um erro ao salvar!" + ex.Message + " " + ex.InnerException;
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        
        // [HttpPost]
        // public async Task<IActionResult> Authenticate(UsuarioModel model)
        // {
        //     try
        //     {
        //         var usuario = _repository.getAll().FirstOrDefault(u => u.Email == model.Email && u.Senha == model.Senha);
        //         if (usuario != null)
        //         {
        //             var tokenHandler = new JwtSecurityTokenHandler();
        //             var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("JwtKey"));
        //             var tokenDescriptor = new SecurityTokenDescriptor
        //             {
        //                 Subject = new ClaimsIdentity(new Claim[]
        //                 {
        //                 new Claim(ClaimTypes.Name, usuario.IdUsuario.ToString())
        //                 }),
        //                 Expires = DateTime.UtcNow.AddDays(7),
        //                 SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //             };
        //             var token = tokenHandler.CreateToken(tokenDescriptor);
        //             var tokenString = tokenHandler.WriteToken(token);

        //             await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
        //                 new ClaimsPrincipal(new ClaimsIdentity(new[]
        //                 {
        //                 new Claim(ClaimTypes.Name, usuario.IdUsuario.ToString()),
        //                 new Claim("token", tokenString)
        //                 }, CookieAuthenticationDefaults.AuthenticationScheme)),
        //                 new AuthenticationProperties
        //                 {
        //                 });

        //             return RedirectToAction("Index", "Home");
        //         }
        //         else
        //         {
        //             ModelState.AddModelError("", "Usuário ou senha inválidos.");
        //             return View("Login", model);
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         ModelState.AddModelError("", "Erro ao autenticar usuário: " + ex.Message);
        //         return View("Login", model);
        //     }
        // }
        [HttpPost]
        public IActionResult Authenticate(string email, string senha)
        {
            try
            {
                var usuario = _repository.getAll().FirstOrDefault(u => u.Email == email && u.Senha == senha);
                if (usuario != null)
                {
                    return RedirectToAction("Index", "Cinema");
                }
                else
                {
                    ModelState.AddModelError("", "Usuário ou senha inválidos.");
                    return View("Login");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao autenticar usuário: " + ex.Message);
                return View("Login");
            }
        }

    }
}
