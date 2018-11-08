using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    public class Projet : Project
    {
        public List<Users> LstUtilisateurs { get; set; }
        public List<File>  LstFile { get; set; }
    }
}
