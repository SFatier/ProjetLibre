using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using API;
using API.Models;
using App.Models;

namespace App.Controllers
{
    public class FilesController : Controller
    {
        private readonly APIContext _context;
       // private readonly FileService _fileService;

        public FilesController(APIContext context/*, FileService fileService*/)
        {
            _context = context;
           // _fileService = fileService;
        }

        // GET: Files
        public async Task<IActionResult> Index()
        {
            return View(await _context.Files.ToListAsync());
        }

        // GET: Files/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var file = await _context.Files
                .FirstOrDefaultAsync(m => m.Id == id);
            if (file == null)
            {
                return NotFound();
            }

            return View(file);
        }

        // GET: Files/Ouvrir
        public async Task<IActionResult> Ouvrir(int? id)
        {
            if (id == null)
            {
                 return NotFound();
            }

            var file = await _context.Files.FirstOrDefaultAsync(m => m.Id == id);
            
            string cmd = "explorer.exe";
            string arg = "/select, " + file.Path;
            System.Diagnostics.Process.Start(cmd, arg);

            return RedirectToAction("Index", "Files");
        }

        // GET: Files/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Files/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Path,Type,DateCreation")] File file)
        {
            if (ModelState.IsValid)
            {
                _context.Add(file);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(file);
        }

        // GET: Files/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var file = await _context.Files.FindAsync(id);
            if (file == null)
            {
                return NotFound();
            }

            #region [Affichage du pdf]
            string filedb =  "C:\\Users\\sigt_sf\\Documents\\GitHub\\ProjetLibre\\Auth\\wwwroot\\file\\pdf_edit.pdf";

            if (System.IO.File.Exists(filedb))
            {
                    System.IO.File.Delete(filedb);
            }

            if (file.Type == "pdf")
                SaveFileInFolder(filedb, System.IO.File.ReadAllBytes(file.Path));

            ViewBag.type = file.Type;
            #endregion

            return View(file);
        }
        
        // POST: Files/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Path,Type,DateCreation")] File file)
        {
            if (id != file.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(file);
                    await _context.SaveChangesAsync();
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var file = await _context.Files
                .FirstOrDefaultAsync(m => m.Id == id);
            if (file == null)
            {
                return NotFound();
            }

            return View(file);
        }

        // POST: Files/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var file = await _context.Files.FindAsync(id);
            _context.Files.Remove(file);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FileExists(int id)
        {
            return _context.Files.Any(e => e.Id == id);
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
