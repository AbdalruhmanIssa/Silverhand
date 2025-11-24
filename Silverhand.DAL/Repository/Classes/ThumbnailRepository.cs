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
    public class TitleRepository:GenericRepository<Title>,ITitleRepository
    {
        public TitleRepository(ApplicationDbContext applicationDbContext):base(applicationDbContext) { }
    }
}
