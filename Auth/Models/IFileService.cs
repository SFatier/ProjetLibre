using System.Collections.Generic;

namespace App.Models
{
    public interface IFileService
    {
        List<File> Get();
        File GetById(int id);
        File Add(File file);
        bool DeleteById(int id);
        bool Update(File file);
    }
}