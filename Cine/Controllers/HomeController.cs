using Cine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Cine.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            FilmeModel model = new FilmeModel();
            return View(model.listar());
        }


        public IActionResult detalhes(int id)
        {
            FilmeModel model = new FilmeModel();
            FilmeModel filme = model.selecionar(id);
            return View(filme);
        }
    }
}
