
using Silverhand.DAL.DTO.Requests;
using Silverhand.DAL.DTO.Responses;
using Silverhand.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.Repository.Repositories
{
    public interface IEpisodeRepository:IGenericRepository<Episode>
    {
        new Episode? GetById(Guid id);
        new IEnumerable<Episode> GetAll(bool withTracking = false);
       



    }
}
