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

            if (context.Project.Count() == 0)
            {
                context.Project.Add(new Project { Nom = "Item1" });
                context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<Project>> Index()
        {
            return objProject.FindAll();
        }

        [HttpGet("{sort}", Name = "GetProjectSort")]
        public ActionResult<List<Project>> Index(string sort)
        {
            return objProject.GetAllProject(sort);
        }

        [HttpGet("{id}", Name = "GetProject")]
        public ActionResult<Project> Details(int id)
        {
            var item = objProject.FindById(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Project item)
        {
            objProject.Insert(item);
            return CreatedAtRoute("GetProject", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Edit([FromBody] Project item)
        {
            var Project = objProject.FindById(item.Id);
            if (Project == null)
            {
                return NotFound();
            }

            Project.Nom = item.Nom;
            //Project.LstFile = item.LstFile;
            //Project.LstUtilisateurs = item.LstUtilisateurs;

            objProject.Update(item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var Project = objProject.FindById(id);
            if (Project == null)
            {
                return NotFound();
            }

            objProject.Delete(Project.Id);
            return NoContent();
        }
    }
}
