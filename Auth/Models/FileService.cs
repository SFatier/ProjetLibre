using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace App.Models
{
    public class FileService : IFileService
    {
        HttpClient _client = new HttpClient();
        private string uri = "https://localhost:44395/api/file";

        /// <summary>
        /// Récupère l'ensemble des projets
        /// </summary>
        /// <returns></returns>
        public List<File> Get()
        {
            var resp = _client.GetAsync(uri).Result;

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<List<File>>(resp.Content.ReadAsStringAsync().Result);
            else
                return null;
        }

        internal File GetById(int id)
        {
            throw new NotImplementedException();
        }

        internal bool Add(File file)
        {
            throw new NotImplementedException();
        }

        internal bool DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        internal bool Update(File file)
        {
            throw new NotImplementedException();
        }

        File IFileService.GetById(int id)
        {
            throw new NotImplementedException();
        }

        bool IFileService.Add(File file)
        {
            throw new NotImplementedException();
        }

        bool IFileService.DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        bool IFileService.Update(File file)
        {
            throw new NotImplementedException();
        }
    }
}
