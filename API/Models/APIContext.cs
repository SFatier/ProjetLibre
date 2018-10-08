using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class APIContext : DbContext
    {
        public APIContext(DbContextOptions options)  : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<File> Files { get; set; }
    }
}
