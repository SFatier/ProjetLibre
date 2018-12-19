using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Auth.Models;
using App.Models;
using GPE;

namespace Auth.Controllers
{
    public class HomeController : Controller
    {
        private DropBox DBB = ReferentielManager.Instance.GetDBB();

        public IActionResult Index()
        {
            DBB.GetDBClient("yJLkyP3VBKAAAAAAAAABcVTOpcwDTZ8csVJn41cUmt2bi0hbkP-bHcFnzDFucWu4");

            ViewBag.Projet = 10;
            ViewBag.Fichier = 10;
            Projet projet = new Projet() { Id = 1, Description = "test", Date = DateTime.Now, Nom = "Net5", Progress = "23%" };
            List<Projet> lst = new List<Projet>();
            lst.Add(projet);
            ViewData["LstProjects"] = lst;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
