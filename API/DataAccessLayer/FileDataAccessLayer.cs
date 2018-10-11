using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    public class FileDataAccessLayer
    {
        private readonly APIContext db;

        public FileDataAccessLayer(APIContext context)
        {
            db = context;
        }

        public List<File>  GetAllFile()
        {
            return db.Files.ToList();
        }
        
        public List<File> GetAllFile(string sort)
        {
            var lst = db.Files.ToList();

            switch (sort)
            {
                case "type":
                    lst = lst.OrderBy(s => s.Type).ToList();
                    break;
                case "type_desc":
                    lst = lst.OrderByDescending(s => s.Type).ToList();
                    break;
                case "date":
                    lst = lst.OrderBy(s => s.DateCreation).ToList();
                    break;
                case "date_desc":
                    lst = lst.OrderByDescending(s => s.DateCreation).ToList();
                    break;
                default:
                    lst = lst.OrderBy(s => s.Nom).ToList();
                    break;
            }

            return lst;
        }


        public File GetById(int id)
        {
            return db.Files.Find(id);
        }

        public void InsertFile(File item)
        {
            db.Files.Add(item);
            db.SaveChanges();
        }

        public void UpdateFile(File file)
        {
            db.Files.Update(file);
            db.SaveChanges();
        }

        public void DeleteFile(File file)
        {
            db.Files.Remove(file);
            db.SaveChanges();
        }
    }
}