using Silverhand.DAL.DTO.Requests;
using Silverhand.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.BLL.Services.Interface
{
    public interface IFavoriteService
    {
        Task<bool> AddAsync(FavoriteRequest request);
        Task<bool> RemoveAsync(Guid profileId, Guid titleId);
        Task<List<FavoriteResponse>> GetAllByProfileAsync(Guid profileId);
    }

}
