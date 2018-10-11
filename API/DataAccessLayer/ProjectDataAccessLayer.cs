using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    public class ProjectDataAccessLayer
    {
        private readonly APIContext db;

        public ProjectDataAccessLayer(APIContext context)
        {
            db = context;
        }

        public List<Project>  GetAllProject()
        {
            return db.Projects.ToList();
        }
        
        public List<Project> GetAllProject(string sort)
        {
            var lst = db.Projects.ToList();

            switch (sort)
            {
                case "nom_desc":
                    lst = lst.OrderByDescending(s => s.Nom).ToList();
                    break;
                case "date":
                    lst = lst.OrderBy(s => s.Date).ToList();
                    break;
                case "date_desc":
                    lst = lst.OrderByDescending(s => s.Date).ToList();
                    break;
                default:
                    lst = lst.OrderBy(s => s.Nom).ToList();
                    break;
            }

            return lst;
        }


        public Project GetById(int id)
        {
            return db.Projects.Find(id);
        }

        public void InsertProject(Project item)
        {
            db.Projects.Add(item);
            db.SaveChanges();
        }

        public void UpdateProject(Project Project)
        {
            db.Projects.Update(Project);
            db.SaveChanges();
        }

        public void DeleteProject(Project Project)
        {
            db.Projects.Remove(Project);
            db.SaveChanges();
        }
    }
}