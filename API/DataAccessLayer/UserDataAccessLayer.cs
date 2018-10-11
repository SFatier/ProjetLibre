using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    public class UserDataAccessLayer
    {
        private readonly APIContext db;

        public UserDataAccessLayer(APIContext context)
        {
            db = context;
        }

        public List<User>  GetAllUser()
        {
            return db.Users.ToList();
        }
        
        //public List<User> GetAllUser(string sort)
        //{
        //    var lst = db.Users.ToList();

        //    switch (sort)
        //    {
        //        case "type":
        //            lst = lst.OrderBy(s => s.Type).ToList();
        //            break;
        //        case "type_desc":
        //            lst = lst.OrderByDescending(s => s.Type).ToList();
        //            break;
        //        case "date":
        //            lst = lst.OrderBy(s => s.DateCreation).ToList();
        //            break;
        //        case "date_desc":
        //            lst = lst.OrderByDescending(s => s.DateCreation).ToList();
        //            break;
        //        default:
        //            lst = lst.OrderBy(s => s.Nom).ToList();
        //            break;
        //    }

        //    return lst;
        //}


        public User GetById(int id)
        {
            return db.Users.Find(id);
        }

        public void InsertUser(User item)
        {
            db.Users.Add(item);
            db.SaveChanges();
        }

        public void UpdateUser(User User)
        {
            db.Users.Update(User);
            db.SaveChanges();
        }

        public void DeleteUser(User User)
        {
            db.Users.Remove(User);
            db.SaveChanges();
        }
    }
}