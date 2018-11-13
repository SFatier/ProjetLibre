using System.Collections.Generic;

namespace App.Models
{
    public interface IGroupeService
    {
        List<Groups> Get();
        Groups GetById(int id);
        bool Add(Groups group);
        bool DeleteById(int id);
        bool Update(Groups group);
    }
}