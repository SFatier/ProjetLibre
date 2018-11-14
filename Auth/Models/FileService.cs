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

        public File GetById(int id)
        {
            var resp = _client.GetAsync(uri + "/" + id).Result;

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<File>(resp.Content.ReadAsStringAsync().Result);
            else
                return null;
        }

        public File Add(File file)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(file), System.Text.Encoding.UTF8, "application/json");
            var resp = _client.PostAsync(uri, content).Result;

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<File>(resp.Content.ReadAsStringAsync().Result);
            else
                return null;
        }

        public bool DeleteById(int id)
        {
            var resp = _client.DeleteAsync(uri + "/" + id).Result;

            if (resp.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public bool Update(File file)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(file), System.Text.Encoding.UTF8, "application/json");

            var resp = _client.PutAsync(uri + "/" + file.Id, content).Result;

            if (resp.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
       
    }
}
