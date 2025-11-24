using Silverhand.DAL.Data;
using Silverhand.DAL.Models;
using Silverhand.DAL.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.Repository.Classes
{
    public class SubtitleRepository:GenericRepository<Subtitle>, ISubtitleRepository
    {
        public SubtitleRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
