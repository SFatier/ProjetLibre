using System.Collections.Generic;

namespace App.Models
{
    internal interface IFileService
    {
        List<File> Get();
        File GetById(int id);
        bool Add(File file);
        bool DeleteById(int id);
        bool Update(File file);
    }
}