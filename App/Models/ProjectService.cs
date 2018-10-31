using API.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace App.Models
{
    public class ProjectService
    {
        HttpClient _client = new HttpClient();
        private string uri = "https://localhost:44395/api/project";
               
        /// <summary>
        /// Récupère l'ensemble des projets
        /// </summary>
        /// <returns></returns>
        public   List<Project> Get()
        {
            var resp = _client.GetAsync(uri).Result;

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<List<Project>>(resp.Content.ReadAsStringAsync().Result);
           else
                return null;
        }

        /// <summary>
        /// Récupère un projet par son id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Project GetById(int id)
        {
            var resp = _client.GetAsync("https://localhost:44395/api/project/"+ id).Result;

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<Project>(resp.Content.ReadAsStringAsync().Result);
            else
                return null;
        }

        /// <summary>
        /// Ajouter un projet
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public bool Add(Project project)
        {
            //string send = JsonConvert.SerializeObject(project);
            StringContent content = new StringContent(JsonConvert.SerializeObject(project), System.Text.Encoding.UTF8, "application/json");
            var resp = _client.PostAsync(uri, content).Result;

            if (resp.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Supprime un projet par id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteById(int id)
        {
            var resp = _client.DeleteAsync("https://localhost:44395/api/project/" + id).Result;

            if (resp.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Modifie un projet
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public bool Update(Project project)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(project), System.Text.Encoding.UTF8, "application/json");

            var resp = _client.PutAsync(uri + "/" + project.Id, content).Result;

            if (resp.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
    }
}
