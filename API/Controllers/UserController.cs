using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API
{
    [Route("api/user")]
    public class UserController : Controller
    {

        private readonly APIContext  _context;

        public UserController(APIContext context)
        {
            _context = context;

            if (_context.Users.Count() == 0)
            {
                _context.Users.Add(new User { FirstName = "Item1" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<User>> GetAll()
        {
            return _context.Users.ToList();
        }

        [HttpGet("{id}", Name = "GetUser")]
        public ActionResult<User> GetById(long id)
        {
            var item = _context.Users.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public IActionResult Create(User item)
        {
            _context.Users.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetUser", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, User item)
        {
            var User = _context.Users.Find(id);
            if (User == null)
            {
                return NotFound();
            }

            User.FirstName = item.FirstName;
            User.LastName = item.LastName;

            _context.Users.Update(User);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var User = _context.Users.Find(id);
            if (User == null)
            {
                return NotFound();
            }

            _context.Users.Remove(User);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
