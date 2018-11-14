using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API
{
    [Route("api/user")]
    public class UserController : Controller
    {
        UserDataAccessLayer objUser;

        public UserController(  APIContext apicontext)
        {
           objUser = new UserDataAccessLayer( apicontext);
        }

        [HttpGet]
        public ActionResult<List<IdentityUser>> Index()
        {
            return objUser.GetAllUser();
        }

        [HttpGet("{id}", Name = "GetUser")]
        public ActionResult<IdentityUser> Details(string id)
        {
            var item = objUser.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpGet("GetUsersByProjectId")]
        public ActionResult<List<IdentityUser>> GetUsersByProjectId(int id)
        {
           List<IdentityUser> lstUsers = objUser.GetUsersByProjectId(id);
            return lstUsers;
        }

        [HttpGet("GetUsersByGroupId")]
        public ActionResult<List<IdentityUser>> GetUsersByGroupId(int id)
        {
            List<IdentityUser>  lst = objUser.GetUsersByGrouptId(id);
            return lst;
        }

        [HttpPost]
        public IActionResult Create(ApplicationUser item)
        {
            objUser.InsertUser(item);
            return CreatedAtRoute("GetUser", new { id = item.Id }, item);
        }

        [HttpPost("InsertUsersByGroupId") ]
        public IActionResult CreateUsersByGroupId(int groupId, string idUser)
        {
            bool istrue =  objUser.InsertUsersByGroupId(idUser, groupId);
            if (!istrue)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost("InsertUsersByProjectId")]
        public IActionResult CreateUsersByProjectId(int projectId, string idUser)
        {
            bool istrue = objUser.InsertUsersByProjectId(idUser, projectId);
            if (!istrue)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Edit(string id, ApplicationUser item)
        {
            var User = objUser.GetById(id);
            if (User == null)
            {
                return NotFound();
            }

            //User.LastName = item.LastName;
           // User.FirstName = item.FirstName;
            User.Email = item.Email;
            User.PasswordHash = item.PasswordHash;

            objUser.UpdateUser(item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
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
