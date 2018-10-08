using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Nom{ get; set; }
        public List<User> LstUtilisateurs { get; set; }
        public List<File>  LstFile { get; set; }

    }
}
