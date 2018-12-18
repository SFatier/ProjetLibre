using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dropbox.Api.Sharing;

namespace GPE
{
    public class File
    {
        public File()
        {

        }

        public File(int _Id, string _IdDropbox, string _IMG, string _Nom, string _Type, DateTime? _DateDeCreation, DateTime? _ModifieLe, string _Taille, bool _IsFile)
        {
            Id = _Id;
            IdDropbox = _IdDropbox;
            IMG = _IMG;
            Nom = _Nom;
            Type = _Type;
            DateCreation = _DateDeCreation;
            ModifieLe = _ModifieLe;
            Taille = _Taille;
            IsFile = _IsFile;
        }

        public File(string _IdDropbox, string _Nom, string _IMG, string _Type, DateTime? _DateDeCreation, DateTime? _ModifieLe, string _Taille, bool _IsFile)
        {
            IdDropbox = _IdDropbox;
            Nom = _Nom;
            IMG = _IMG;
            Type = _Type;
            DateCreation = _DateDeCreation;
            ModifieLe = _ModifieLe;
            Taille = _Taille;
            IsFile = _IsFile;
        }

        public int Id { get; set; }
        public string IdDropbox { get; set; }
        public string Nom { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        public DateTime? DateCreation{ get; set; }
        public DateTime? ModifieLe { get; internal set; }
        public string IMG { get; set; }
        public string Taille { get; set; }
        public string ImageCaption { set; get; }
        public string ImageDescription { set; get; }
        public string PreviewUrl { get; internal set; }
        public DateTime? DateInvitation { get; internal set; }
        public bool isInProject;
        public string path;
        public bool IsFile { get; set; }
    }
}
