using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Models;
using Microsoft.Extensions.Configuration;
using GPE;
using File = App.Models.File;

namespace App.Controllers
{
    public class FilesController : Controller
    {
        private List<File> lstfiles = new List<File>();
        private DropBox DBB = ReferentielManager.Instance.GetDBB();
        
        public IConfiguration Configuration { get; set; }

        public FilesController(IConfiguration config)
        {
            Configuration = config;
            lstfiles = ReferentielManager.Instance.GetAllFile();
        }

        // GET: Files
        public IActionResult Index(int selected = 1)
        {
           ViewBag.btnGroup = selected;

            ViewData["_Index_Local"] = lstfiles.Where(f => f.Id != 0).ToList();
            ViewData["_Index_Dropbox"] = lstfiles.Where(f => f.Id ==  0).ToList();
          
            return View(lstfiles);
        }

        // GET: Files/Details/5
        public IActionResult Details(int id)
        {
            var file = lstfiles.Find(f => f.Id.Equals(id) || f.IdDropbox.Equals(id));

            if (file.Id != 0)
            {
                if (file == null)
                {
                    return NotFound();
                }
            }

            return View(file);
        }

        // GET: Files/Visualiser/5
        public IActionResult Visualiser(int id)
        {
            var file = lstfiles.Find(f =>f.IdDropbox.Equals(id));
            
            return View(file);
        }

        // GET: Files/Telecharger/5
        public IActionResult Telecharger(int id, string pathlocal)
        {
            var file = lstfiles.Find(f => f.IdDropbox.Equals(id));
            DBB.Download(file.Path, file.Nom, pathlocal, file.Nom);
            return RedirectToAction(nameof(Index)); 
        }

        // POST: Files/Importer/5
        [HttpPost]
        public async Task<IActionResult> Importer(string path, string file_name, Microsoft.AspNetCore.Http.IFormFile path_local)
        {
            File file = new File() { Path = Configuration["folderTmp"] + path_local.FileName, Nom = path_local.FileName, MyImage = path_local };

            using (var stream = new System.IO.FileStream(file.Path, System.IO.FileMode.Create))
            {
                await file.MyImage.CopyToAsync(stream);
            }

            DBB.Upload(path, path_local.FileName, file.Path);
            return RedirectToAction(nameof(Index));
        }

        // GET: Files/Ouvrir
        public IActionResult Ouvrir(int id)
        {
            var file = lstfiles.Find(f => f.Id.Equals(id));

            if (file.Id != 0) { 
                string cmd = "explorer.exe";
                string arg = "/select, " + file.Path;
                System.Diagnostics.Process.Start(cmd, arg);
            }

            return RedirectToAction("Index", "Files");
        }

        // GET: Files/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Files/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom, MyImage")] File file)
        {
            if (ModelState.IsValid)
            {
                file.Path = Configuration["folderTmp"] + file.MyImage.FileName;

                using (var stream = new System.IO.FileStream(file.Path, System.IO.FileMode.Create))
                {
                    await file.MyImage.CopyToAsync(stream);
                }
                file.DateCreation = DateTime.Now;
                file.Type = System.IO.Path.GetExtension(file.Path) ;
                ReferentielManager.Instance.AddFile(file);
                return RedirectToAction(nameof(Index));
            }
            return View(file);
        }

        // GET: Files/Edit/5
        public IActionResult Edit(int id)
        {
            var file = lstfiles.Find(f => f.Id.Equals(id) || f.IdDropbox.Equals(id));

            if (file.Id != 0)
            {
                if (file == null)
                {
                    return NotFound();
                }
            }

            #region [Affichage du pdf]
            string filedb = Configuration["folderPDF"];

            //if (System.IO.File.Exists(filedb))
            //{
            //        System.IO.File.Delete(filedb);
            //}

            if (file.Type == "pdf")
                SaveFileInFolder(filedb, System.IO.File.ReadAllBytes(file.Path));

            ViewBag.type = file.Type;
            #endregion

            return View(file);
        }
        
        // POST: Files/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nom,Path,Type,DateCreation")] File file)
        {
            if (id != file.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ReferentielManager.Instance.UpdateFile(file);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FileExists(file.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(file);
        }

        // GET: Files/Delete/5
        public IActionResult Delete(int id)
        {
            var file = lstfiles.Find(f => f.Id.Equals(id) || f.IdDropbox.Equals(id));

            if (file.Id != 0)
            {
                if (file == null)
                {
                    return NotFound();
                }
            }
            else
            {
                DBB.Delete(file.Path);
            }
            return View(file);
        }

        // POST: Files/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            ReferentielManager.Instance.DeleteFile(id);
            return RedirectToAction(nameof(Index));
        }

        private bool FileExists(int id)
        {
            var file = lstfiles.Find(f => f.Id.Equals(id) || f.IdDropbox.Equals(id));

            if (file != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        /// <summary>
        /// Creer le doc dans le dossier wwwroot/pdf
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="file"></param>
        private static void SaveFileInFolder(string filename, byte[] file)
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(filename);
            using (System.IO.FileStream filestream = fi.Create())
            {
                filestream.Write(file, 0, file.Length);
            }
        }
    }
}
