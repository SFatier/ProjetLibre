using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    public class Users
    {
        public string Id{ get; set; }
        public string Username{ get; set; }
        public string Email{ get; set; }
        public string role{ get; set; }
        public bool isInProject { get; set; }
    }

    public class Groups : Group
    {
        public List<Users> Users { get; set; }
        public int NbUsers { get; set; }
    }
}
