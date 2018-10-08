using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly APIContext _context;

        public FileController(APIContext context)
        {
            _context = context;

            if (_context.Files.Count() == 0)
            {
                _context.Files.Add(new File { Nom = "Item1" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<File>> GetAll()
        {
            return _context.Files.ToList();
        }

        [HttpGet("{id}", Name = "GetFile")]
        public ActionResult<File> GetById(long id)
        {
            var item = _context.Files.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public IActionResult Create(File item)
        {
            _context.Files.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetFile", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, File item)
        {
            var File = _context.Files.Find(id);
            if (File == null)
            {
                return NotFound();
            }

            File.Nom = item.Nom;
            File.Path = item.Path;

            _context.Files.Update(File);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var File = _context.Files.Find(id);
            if (File == null)
            {
                return NotFound();
            }

            _context.Files.Remove(File);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
