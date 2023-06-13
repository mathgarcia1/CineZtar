// <copyright file="HomeController.cs" company="CineZtarCompany">
// Copyright (c) CineZtarCompany. All rights reserved.
// </copyright>
/// <summary>
/// Description of the class or file.
/// </summary>
/// <author>mathgarcia1</author>
/// <created>2023-05-31 13:43:37</created>
/// <lastModified>2023-05-31 13:43:37</lastModified>
namespace Cine.Controllers
{
    using Cine.Models;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            FilmeModel model = new ();
            return this.View(model.Listar());
        }

        public IActionResult detalhes(int id)
        {
            FilmeModel model = new ();
            FilmeModel filme = model.Selecionar(id);
            return this.View(filme);
        }
    }
}
