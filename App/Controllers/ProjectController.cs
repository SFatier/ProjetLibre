using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using API.Models;
using App.Data;
using API.Controllers;
using API.DataAccessLayer;
using App.Models;

namespace App.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ProjectService _service;

        public ProjectController(ProjectService projectService)
        {
            _service = projectService; ;
        }

        // GET: Project
        public IActionResult Index()
        {
            return View(_service.Get());
        }

        // GET: Project/Details/5
        public IActionResult Details(int id)
        {
            var project = _service.GetById(id);

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Project/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Project/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Nom,Date")] Project project)
        {
            if (ModelState.IsValid)
            {
                _service.Add(project);
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Project/Edit/5
        public IActionResult Edit(int id)
        {            
            var project = _service.GetById(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Project/Edit/5       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nom,Date")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _service.Update(project);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Project/Delete/5
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = _service.GetById(id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var project = _service.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            if (_service.GetById(id) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
