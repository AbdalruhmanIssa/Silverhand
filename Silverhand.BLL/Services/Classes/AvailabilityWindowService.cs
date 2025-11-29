using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.BLL.Services.Classes
{
    using Mapster;
    using Silverhand.BLL.Services.Interface;
    using Silverhand.DAL.DTO.Requests;
    using Silverhand.DAL.DTO.Responses;
    using Silverhand.DAL.Models;
    using Silverhand.DAL.Repository.Repositories;

    public class AvailabilityWindowService : IAvailabilityWindowService
    {
        private readonly IAvailabilityWindowRepository _repo;

        public AvailabilityWindowService(IAvailabilityWindowRepository repo)
        {
            _repo = repo;
        }

        public async Task<AvailabilityWindowResponse> CreateAsync(AvailabilityWindowRequest request)
        {
            var entity = request.Adapt<AvailabilityWindow>();
            var saved = await _repo.AddAsync(entity);
            return saved.Adapt<AvailabilityWindowResponse>();
        }

        public async Task<List<AvailabilityWindowResponse>> GetForTitleAsync(Guid titleId)
        {
            var list = await _repo.GetForTitleAsync(titleId);
            return list.Adapt<List<AvailabilityWindowResponse>>();
        }

        public async Task<List<AvailabilityWindowResponse>> GetForEpisodeAsync(Guid episodeId)
        {
            var list = await _repo.GetForEpisodeAsync(episodeId);
            return list.Adapt<List<AvailabilityWindowResponse>>();
        }
    }

}
