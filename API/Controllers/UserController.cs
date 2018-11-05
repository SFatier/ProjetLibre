using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API
{
    [Route("api/user")]
    public class UserController : Controller
    {
        UserDataAccessLayer objUser;

        public UserController(APIContext context)
        {
            objUser = new UserDataAccessLayer(context);

            if (context.Users.Count() == 0)
            {
                context.Users.Add(new ApplicationUser { FirstName = "Item1", LastName="Item2" });
                context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<ApplicationUser>> Index()
        {
            return objUser.GetAllUser();
        }

        //[HttpGet("{sort}", Name = "GetUserSort")]
        //public ActionResult<List<User>> Index(string sort)
        //{
        //    return objUser.GetAllUser(sort);
        //}

        [HttpGet("{id}", Name = "GetUser")]
        public ActionResult<ApplicationUser> Details(int id)
        {
            var item = objUser.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public IActionResult Create(ApplicationUser item)
        {
            objUser.InsertUser(item);
            return CreatedAtRoute("GetUser", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, ApplicationUser item)
        {
            var User = objUser.GetById(id);
            if (User == null)
            {
                return NotFound();
            }

            User.LastName = item.LastName;
            User.FirstName = item.FirstName;
            User.Email = item.Email;
            User.PasswordHash = item.PasswordHash;

            objUser.UpdateUser(item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var User = objUser.GetById(id);
            if (User == null)
            {
                return NotFound();
            }

            objUser.DeleteUser(User);
            return NoContent();
        }

    }
}
