using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    public class Users
    {
        public string UserId{ get; set; }
        public string Username{ get; set; }
        public string Email{ get; set; }
        public string role{ get; set; }
    }

    public class Groups
    {
        public string Nom { get; set; }
        public List<Users> Users { get; set; }
    }
}
