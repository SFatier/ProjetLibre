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
            return Container.Projects.ToList();
        }
        
        public List<Project> GetAllProject(string sort)
        {
            var lst = Container.Projects.ToList();

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
            return Container.Projects.Find(id);
        }

        public void Insert(Project item)
        {
            Container.Projects.Add(item);
            Container.SaveChanges();
        }

        public void Update(Project Project)
        {
            Container.Projects.Update(Project);
            Container.SaveChanges();
        }

        public void Delete(int id)
        {
            Project project = Container.Projects.Find(id);
            Container.Projects.Remove(project);
            Container.SaveChanges();
        }
    }
}