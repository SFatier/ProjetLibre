﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Nom{ get; set; }
        //public List<ApplicationUser> LstUtilisateurs { get; set; }
        //public List<File>  LstFile { get; set; }
        public string Description { get; set; }
        public DateTime Date{ get; set; }
    }
}
