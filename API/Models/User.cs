using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace API
{
    public enum Profil{
        Chef_de_projet = 0,
        Administrateur = 1,
        Gestionnaire_de_cloud = 2,
        Secretaire = 3,
        Etudiant = 4
    }

    public class User
    {
        public int  Id { get; set; }
        public string  LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public Profil role { get; set; }

        private string password;
        public string Password {
            get
            {
                return password;
            }

            set
            {
                password = Encrypt.base64Encode(value); ;
            }
        }
    }
}
