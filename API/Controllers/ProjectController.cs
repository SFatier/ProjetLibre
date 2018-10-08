using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API
{
    [Route("api/Project")]
    public class ProjectController : Controller
    {

        private readonly APIContext  _context;

        public ProjectController(APIContext context)
        {
            _context = context;

            if (_context.Projects.Count() == 0)
            {
                _context.Projects.Add(new Project { Nom = "Item1" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<Project>> GetAll()
        {
            return _context.Projects.ToList();
        }

        [HttpGet("{id}", Name = "GetProject")]
        public ActionResult<Project> GetById(long id)
        {
            var item = _context.Projects.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public IActionResult Create(Project item)
        {
            _context.Projects.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetProject", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, Project item)
        {
            var Project = _context.Projects.Find(id);
            if (Project == null)
            {
                return NotFound();
            }

            Project.Nom = item.Nom;
            Project.LstUtilisateurs = item.LstUtilisateurs;
            Project.LstFile = item.LstFile;

            _context.Projects.Update(Project);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var Project = _context.Projects.Find(id);
            if (Project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(Project);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
