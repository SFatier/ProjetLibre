using API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace App.Models
{

    public class UserService : IUserService
    {

        public UserService()
        {

        }

        HttpClient _client = new HttpClient();
        private string uri = "https://localhost:44395/api/user";

        /// <summary>
        /// Récupère l'ensemble des users
        /// </summary>
        /// <returns></returns>
        public List<Users> Get()
        {
            var resp = _client.GetAsync(uri).Result;

            if (resp.IsSuccessStatusCode)
            {
                string lst = resp.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<Users>>(lst);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Récupère un user par son id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Users GetById(string id)
        {
            var resp = _client.GetAsync("https://localhost:44395/api/user/" + id).Result;

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<Users>(resp.Content.ReadAsStringAsync().Result);
            else
                return null;
        }

        /// <summary>
        /// Ajouter un user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Add(Users user)
        {
            //string send = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(JsonConvert.SerializeObject(user), System.Text.Encoding.UTF8, "application/json");
            var resp = _client.PostAsync(uri, content).Result;

            if (resp.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Supprime un user par id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteById(int id)
        {
            var resp = _client.DeleteAsync("https://localhost:44395/api/user/" + id).Result;

            if (resp.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Modifie un user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Update(Users user)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(user), System.Text.Encoding.UTF8, "application/json");

            var resp = _client.PutAsync(uri + "/" + user.Id, content).Result;

            if (resp.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Recupère tous les utilsateurs d'un projet
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Users> GetUsersByProjectId(int id)
        {
            var resp = _client.GetAsync(uri + "/GetUsersByProjectId/" + id).Result; //_context.Users.FromSql("EXECUTE  GetUsersByProjectId {0} ", id).ToList()

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<List<Users>>(resp.Content.ReadAsStringAsync().Result);
            else
                return null;
        }
    }
}
