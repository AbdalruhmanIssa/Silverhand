using Microsoft.EntityFrameworkCore;
using Silverhand.DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Title> Titles { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    }
}
