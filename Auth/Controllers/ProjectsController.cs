using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Models;
using System.Linq;

namespace App.Controllers
{
    public class ProjectsController : Controller
    {
         // GET: Projects
        public  IActionResult Index()
        {
           List<Projet> lstproject = ReferentielManager.Instance.GetAllProjet();
            ViewData["lstproject"] = lstproject;
            return View(lstproject);
        }

        public PartialViewResult ShowGridOrTab()
        {
            List<Projet> lstproject = ReferentielManager.Instance.GetAllProjet();
            return PartialView("_GridProjects", lstproject);
        }

        // GET: Projects/Details/5
        public IActionResult Details(int id)
        {
            var project = ReferentielManager.Instance.GetProjetById(id);
            var users = ReferentielManager.Instance.GetUsersByProjectId(id);
            var files = ReferentielManager.Instance.GetFilesByProjectId(id);

            ViewBag.lstUsers = users;
            ViewBag.lstFiles = files;
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
        public IActionResult Create([Bind("Id,Nom,Description")] Projet projet, List<string> states, List<int> files)
        {
            if (ModelState.IsValid)
            {
                projet.Date = DateTime.Now;
                Projet projetResult = ReferentielManager.Instance.AddProjet(projet);
               
                //recupertion des utilisateurs
                foreach (string item in states)
                {
                    ReferentielManager.Instance.InsertUsersByProjectId(projetResult.Id, item);
                }

                // recuperation des fichiers
                foreach (int item in files)
                {
                    ReferentielManager.Instance.InsertFilesByProjectId(  projetResult.Id, item);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(projet);
        }

        // GET: Projects/Edit/5
        public IActionResult Edit(int id)
        {
            var project = ReferentielManager.Instance.GetProjetById(id);

           var lst_users = ReferentielManager.Instance.GetAllUsers();
           List<Users> users_project = ReferentielManager.Instance.GetUsersByProjectId(id);
           var list3 = lst_users.Except(users_project).ToList();

            var lst_files = ReferentielManager.Instance.GetAllFile();
            List<File> files_project = ReferentielManager.Instance.GetFilesByProjectId(id);
            var list4 = lst_files.Except(files_project).ToList();

            ViewBag.lstUsers = users_project;
            ViewBag.lstFiles = files_project;
            ViewBag.lstUsersOld = list3;
            ViewBag.lstFilesOld = list4;

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
        public IActionResult Edit(int id, [Bind("Id,Nom,Description,Date")] Projet projet, List<string> states, List<int> files)
        {
            if (ModelState.IsValid)
            {
                Projet presult = ReferentielManager.Instance.GetProjetById(projet.Id);

                projet.Date = presult.Date;
                 ReferentielManager.Instance.UpdateProjet(projet);

                var lstusers = ReferentielManager.Instance.GetUsersByProjectId(id);
                var lstfiles = ReferentielManager.Instance.GetFilesByProjectId(id);

                //recupertion des utilisateurs
                var list3 = lstusers.Select(x => x.Id).Except(states);  // list a supprimé
                foreach (var item in list3)
                {
                    ReferentielManager.Instance.DeleteUsersByProjectId(projet.Id, item);  //delier un utilisateur à un projet
                }

                var list4 = states.Except(lstusers.Select(x => x.Id));
                var resultList = list3.Concat(list4).ToList();
                foreach (string item in resultList)
                {
                    ReferentielManager.Instance.InsertUsersByProjectId(projet.Id, item);
                }

                // recuperation des fichiers
                var list5 = lstfiles.Select(x => x.Id).Except(files); // list a supprimé
                foreach (var item in list3)
                {
                    ReferentielManager.Instance.DeleteFilesByProjectId(projet.Id, item);  //delier un fichier à un projet
                }

                var list6 = files.Except(lstfiles.Select(x => x.Id));
                var resultListFiles = list5.Concat(list6).ToList();
                foreach (int item in resultListFiles)
                {
                    ReferentielManager.Instance.InsertFilesByProjectId(projet.Id, item);
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
