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
    public class ThumbnailRepository : GenericRepository<Thumbnail>,IThumbnailRepository
    {
        public ThumbnailRepository(ApplicationDbContext applicationDbContext):base(applicationDbContext) { }
    }
}
