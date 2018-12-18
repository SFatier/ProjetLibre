using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHM.Model
{
    public class Utilisateur 
    {
        private string nom;
        private DateTime dateDeCreation = DateTime.Now;
        private List<GPE.File> lstFiles;
        private bool isChecked = false;

        private string token_DP;
        private string token_GG;
        private string refreshToken;
        private string login;
        private string email;
        private string mdp;
        private string role;
        public string Email { get; internal set; }
    }
}
