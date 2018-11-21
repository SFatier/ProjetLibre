﻿using System.Collections.Generic;

namespace App.Models
{
    internal interface IProjetService
    {
        List<Projet> Get();
        Projet GetById(int id);
        bool Add(Projet projet);
        bool DeleteById(int id);
        bool Update(Projet projet);
    }
}