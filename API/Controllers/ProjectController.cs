using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API
{
    [Route("api/Project")]
    public class ProjectController : Controller
    {
        ProjectDataAccessLayer objProject;

        public ProjectController(APIContext context)
        {
            objProject = new ProjectDataAccessLayer(context);

            if (context.Projects.Count() == 0)
            {
                context.Projects.Add(new Project { Nom = "Item1" });
                context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<Project>> Index()
        {
            return objProject.GetAllProject();
        }

        [HttpGet("{sort}", Name = "GetProjectSort")]
        public ActionResult<List<Project>> Index(string sort)
        {
            return objProject.GetAllProject(sort);
        }

        [HttpGet("{id}", Name = "GetProject")]
        public ActionResult<Project> Details(int id)
        {
            var item = objProject.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public IActionResult Create(Project item)
        {
            objProject.InsertProject(item);
            return CreatedAtRoute("GetProject", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, Project item)
        {
            var Project = objProject.GetById(id);
            if (Project == null)
            {
                return NotFound();
            }

            Project.Nom = item.Nom;
            Project.LstFile = item.LstFile;
            Project.LstUtilisateurs = item.LstUtilisateurs;

            objProject.UpdateProject(item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var Project = objProject.GetById(id);
            if (Project == null)
            {
                return NotFound();
            }

            objProject.DeleteProject(Project);
            return NoContent();
        }
    }
}
