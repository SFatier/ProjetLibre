using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API;
using App.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{
    public class UsersController : Controller
    {

        private readonly APIContext _context;

        public UsersController(APIContext context)
        {
            _context = context;
        }

        // GET: Users
        public ActionResult Index()
        {
            var usersWithRoles = _context.Users.FromSql("EXECUTE  GetAllUsers ").ToList();
            List<Users> lstUsers = usersWithRoles.Select(x => new Users { UserId = x.Id, Username = x.UserName, Email = x.Email }).ToList();
            return View(lstUsers);
        }

        // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            var user = _context.Users.FromSql("EXECUTE  GetUsersById {0}", id).FirstOrDefault();
            return View(new Users() { UserId = user.Id, Username = user.UserName, Email = user.Email, role = "" });
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Username,Email,role")] Users user)
        {
            try
            {
               // _context.Users.FromSql("EXECUTE  DeleteUser {0}", id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            var user = _context.Users.FromSql("EXECUTE  GetUsersById {0}", id).FirstOrDefault();
            return View(new Users() { UserId = user.Id, Username = user.UserName, Email = user.Email, role = "" });
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("UserId,Username,Email,role")]Users user)
        {
            try
            {
                // TODO: Add update logic here
                _context.Users.FromSql("EXECUTE  UpdateUser {0},{1},{2},{3} ", user.UserId, user.Username, user.Email, user.role).ToList();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
           var user =  _context.Users.FromSql("EXECUTE  GetUsersById {0}", id).FirstOrDefault();        
            return View(new Users() { UserId = user.Id, Username = user.UserName, Email = user.Email,  role = ""});
        }

        // POST: Users/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                _context.Users.FromSql("EXECUTE  DeleteUser {0}", id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}