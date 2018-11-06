using API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DataAccessLayer
{
    public class ReferentielManager
    {
        public static APIContext ct = null;
        private static ReferentielManager s_Instance;
        private static readonly object s_InstanceLocker = new object();

        private readonly IProject _projectRepo;

        #region [ Singleton ]

        //public static ReferentielManager Instance
        //{
        //    get
        //    {
        //        lock (s_InstanceLocker)
        //        {
        //            if (s_Instance == null)
        //                ct = new APIContext();
        //            s_Instance = new ReferentielManager();
        //        }
        //        return s_Instance;
        //    }
        //}

        #endregion [ Singleton ]

        private ReferentielManager()
        {
            _projectRepo = new ProjectDataAccessLayer(ct);
        }

        public List<Project> GetAllProject()
        {
            return _projectRepo.FindAll();
        }

     }
}
