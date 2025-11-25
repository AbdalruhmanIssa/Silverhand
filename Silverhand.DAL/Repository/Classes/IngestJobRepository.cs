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
    public class IngestJobRepository:GenericRepository<IngestJob>,IIngestJobRepository
    {
        public IngestJobRepository(ApplicationDbContext applicationDbContext):base(applicationDbContext) { }
    }
}
