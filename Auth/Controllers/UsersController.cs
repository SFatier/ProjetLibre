using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API;
using App.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace App.Controllers
{
    public class UsersController : Controller
    {

        private readonly APIContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(APIContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Users
        public ActionResult Index()
        {
            var usersWithRoles = _context.Users.FromSql("EXECUTE  GetAllUsers ").ToList();
            List<Users> lstUsers = usersWithRoles.Select(x => new Users { UserId = x.Id, Username = x.UserName, Email = x.Email }).ToList();
            List<Groups> groups = new List<Groups>();
            var lstgroups = _context.Groups.ToList();
            lstgroups.ForEach(x =>
            {
                var users = _context.Users.FromSql("EXECUTE  GetUserByGroupId {0} ", x.Id).ToList();
                groups.Add(new Groups() { Id = x.Id, Nom = x.Nom, NbUsers = users.Count() });
            });
            
            ViewBag.Groups = groups;
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
        
        //Get: Users/CreateGroup
       public ActionResult CreateGroup()
        {
            ViewBag.lstUsers = _context.Users.Select(x => new Users() { UserId = x.Id, Username = x.UserName }).ToList();
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Username,Email,role")] Users user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IdentityUser identityUser = new IdentityUser { UserName = user.Username, Email = user.Email };
                    string newPassword = GenerateRandomPassword(null);
                    var result = await _userManager.CreateAsync(identityUser, newPassword);

                }
                    return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: Users/CreateGroup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGroup([Bind("Nom,Users")] Groups groups, List<string> states)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    API.Models.Group group = new API.Models.Group() { Nom = groups.Nom };
                    _context.Groups.Add(group);
                    await _context.SaveChangesAsync();
                    
                    foreach (var item in states)
                    {
                        _context.Users.FromSql("EXECUTE  [InsertUsersByGroupId] {0},{1} ", group.Id, item);
                        await _context.SaveChangesAsync();
                    }

                }
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

        #region GeneratePassword
        public static string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
                    "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
                    "abcdefghijkmnopqrstuvwxyz",    // lowercase
                    "0123456789",                   // digits
                    "!@$?_-"                        // non-alphanumeric
    };
            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
        #endregion
    }
}
 