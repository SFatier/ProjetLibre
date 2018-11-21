using System.Collections.Generic;

namespace App.Models
{
    internal interface IUserService
    {
        List<Users> Get();
        Users GetById(string id);
        bool Add(Users users);
        bool DeleteById(int id);
        bool Update(Users users);
    }
}