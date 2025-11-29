using Silverhand.DAL.DTO.Requests;
using Silverhand.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.BLL.Services.Interface
{
    public interface IAvailabilityWindowService
    {
        Task<AvailabilityWindowResponse> CreateAsync(AvailabilityWindowRequest request);
        Task<List<AvailabilityWindowResponse>> GetForTitleAsync(Guid titleId);
        Task<List<AvailabilityWindowResponse>> GetForEpisodeAsync(Guid episodeId);
    }

}
