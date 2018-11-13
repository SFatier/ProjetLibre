using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Models;

namespace App.Controllers
{
    public class ProjectsController : Controller
    {
         // GET: Projects
        public  IActionResult Index()
        {
           List<Projet> lstproject = ReferentielManager.Instance.GetAllProjet();
            return View(lstproject);
        }

        // GET: Projects/Details/5
        public IActionResult Details(int id)
        {
            var project = ReferentielManager.Instance.GetProjetById(id);
            var users = ReferentielManager.Instance.GetUsersByProjectId(id);  
            ViewBag.lstUsers = users;
            return View(project);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            ViewBag.lstUsers = ReferentielManager.Instance.GetAllUsers();
            ViewBag.lstFiles = ReferentielManager.Instance.GetAllFile();
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Nom,Description")] Projet projet, List<string> states)
        {
            if (ModelState.IsValid)
            {
                projet.Date = DateTime.Now;
                ReferentielManager.Instance.AddProjet(projet);
                return RedirectToAction(nameof(Index));
            }
            return View(projet);
        }

        // GET: Projects/Edit/5
        public IActionResult Edit(int id)
        {
            var project = ReferentielManager.Instance.GetProjetById(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nom,Description,Date")] Projet projet)
        {
            if (id != projet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ReferentielManager.Instance.UpdateProjet(projet);
                }
                catch (DbUpdateConcurrencyException)
                {
                 //
                }
                return RedirectToAction(nameof(Index));
            }
            return View(projet);
        }

        // GET: Projects/Delete/5
        public IActionResult Delete(int id)
        {
            var project = ReferentielManager.Instance.GetProjetById(id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var project = ReferentielManager.Instance.GetProjetById(id);
            ReferentielManager.Instance.DeleteProjet(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
