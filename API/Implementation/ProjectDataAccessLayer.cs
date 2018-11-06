using API.DataAccessLayer;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    public class ProjectDataAccessLayer : Repository<Project>, IProject
    {
        #region [Constructeur]
        public ProjectDataAccessLayer(APIContext container) : base(container)
        {
        }        
         #endregion

        public List<Project>  FindAll()
        {
            return Container.Project.ToList();
        }
        
        public List<Project> GetAllProject(string sort)
        {
            var lst = Container.Project.ToList();

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
        
        public Project FindById(int id)
        {
            return Container.Project.Find(id);
        }

        public void Insert(Project item)
        {
            Container.Project.Add(item);
            Container.SaveChanges();
        }

        public void Update(Project Project)
        {
            Container.Project.Update(Project);
            Container.SaveChanges();
        }

        public void Delete(int id)
        {
            Project project = Container.Project.Find(id);
            Container.Project.Remove(project);
            Container.SaveChanges();
        }
    }
}