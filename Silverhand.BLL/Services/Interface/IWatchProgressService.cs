using Silverhand.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.BLL.Services.Interface
{
    public interface IWatchProgressService
    {
        Task<WatchProgressResponse> UpsertAsync(WatchProgressRequest request);
        Task<List<WatchProgressResponse>> GetForProfileAsync(Guid profileId);
    }

}
