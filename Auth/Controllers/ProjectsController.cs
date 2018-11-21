using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Web.WebPages;
using Microsoft.EntityFrameworkCore;
using App.Models;
using System.Web;
using PagedList;
using PagedList.Mvc;
using System.Linq;

namespace App.Controllers
{
    public class ProjectsController : Controller
    {
        // GET: Projects
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            //  List<Projet> lstproject = ReferentielManager.Instance.GetAllProjet();
            //  return View(lstproject);
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            List<Projet> lstproject = ReferentielManager.Instance.GetAllProjet();
            if (!String.IsNullOrEmpty(searchString))
            {
                var matchingvalues = lstproject.Where(s => s.Nom.Contains(searchString, StringComparison.OrdinalIgnoreCase));
                //IEnumerable<string> result = lstproject.Where(s => s.Contains(search));
                //   var resultList = lstproject.FindAll(delegate (string s) { return s.Contains(searchString); });
                //lstproject = lstproject.Contains(searchString);
                // lstproject = lstproject.Where(s => s.Nom.Contains(searchString) || s.Description.Contains(searchString)).ToList();
                return View(matchingvalues);
            }
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            switch (sortOrder)
            {
                case "name_desc":
                    //   students = students.OrderByDescending(s => s.LastName);
                    var newList = lstproject.OrderByDescending(x => x.Nom);
                    return View(newList);
                    break;
                case "Date":
                    var newListDate = lstproject.OrderBy(x => x.Date).ToList();
                    return View(newListDate);
                    break;
                case "date_desc":
                    var newListDes = lstproject.OrderByDescending(x => x.Description);
                    return View(newListDes);
                    break;
                default:
                    var newListDefault = lstproject.OrderByDescending(x => x.Nom).ToList();
                    break;
            }
            //return View(lstproject);
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(lstproject.ToPagedList(pageNumber, pageSize));

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
