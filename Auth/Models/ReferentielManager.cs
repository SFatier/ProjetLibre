using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    public class ReferentielManager
    {
        private  IProjetService _service;
        private  IUserService _serviceUser;
        private  IFileService _serviceFile;
        private IGroupeService _serviceGroups;

        #region [ Singleton ]

        private static ReferentielManager s_Instance;
        private static readonly object s_InstanceLocker = new object();

        public static ReferentielManager Instance 
        {
            get
             {
                if (s_Instance == null)
                {
                    s_Instance = new ReferentielManager( );
                }
                return s_Instance;
            }
        }

        #endregion [ Singleton ]

        private ReferentielManager()
        {
            _service = new ProjetService();
            _serviceUser = new UserService();
            _serviceFile = new FileService();
            _serviceGroups = new GroupService();
        }
        
        /*Projet*/
        public List<Projet> GetAllProjet()
        {
            return _service.Get();
        }

        public Projet GetProjetById(int id)
        {
            return _service.GetById(id);
        }

        public bool AddProjet(Projet projet)
        {
            return _service.Add(projet);
        }

        public bool DeleteProjet(int id)
        {
            return _service.DeleteById(id);
        }

        public bool UpdateProjet(Projet projet)
        {
            return _service.Update(projet);
        }

        /*Users*/
        public List<Users> GetUsersByProjectId(int id)
        {
            List<Users> users = new List<Users>();
            return users;
        }

        public List<Users> GetAllUsers()
        {
            return _serviceUser.Get();
        }

        public Users GetUsersById(string id)
        {
            return _serviceUser.GetById(id);
        }

        public bool AddUser(Users Users)
        {
            return _serviceUser.Add(Users);
        }

        public bool DeleteUsers(int id)
        {
            return _serviceUser.DeleteById(id);
        }

        public bool UpdateUsers(Users Users)
        {
            return _serviceUser.Update(Users);
        }
        
        /*File*/
        public List<File> GetAllFile()
        {
            return _serviceFile.Get();
        }
               
        public File GetFileById(int id)
        {
            return _serviceFile.GetById(id);
        }

        public bool AddFile(File file)
        {
            return _serviceFile.Add(file);
        }

        public bool DeleteFile(int id)
        {
            return _serviceFile.DeleteById(id);
        }

        public bool UpdateFile(File file)
        {
            return _serviceFile.Update(file);
        }

        /*Group*/
        public List<Groups> GetAllGroups()
        {
            return _serviceGroups.Get();
        }

        public Groups GetGroupsById(int id)
        {
            return _serviceGroups.GetById(id);
        }

        public bool AddGroups(Groups Groups)
        {
            return _serviceGroups.Add(Groups);
        }

        public bool DeleteGroups(int id)
        {
            return _serviceGroups.DeleteById(id);
        }

        public bool UpdateGroups(Groups Groups)
        {
            return _serviceGroups.Update(Groups);
        }
    }
}
