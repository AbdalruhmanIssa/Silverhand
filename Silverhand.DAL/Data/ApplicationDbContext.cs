using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Silverhand.DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public DbSet<Title> Titles { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Subtitle> Subtitles { get; set; }
        public DbSet<Thumbnail> Thumbnails { get; set; }
        public DbSet<IngestJob> IngestJobs { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    }
}
