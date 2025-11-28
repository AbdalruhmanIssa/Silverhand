using Mapster;
using Silverhand.BLL.Services.Interface;
using Silverhand.DAL.DTO.Responses;
using Silverhand.DAL.Models;
using Silverhand.DAL.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.BLL.Services.Classes
{
    public class WatchProgressService : IWatchProgressService
    {
        private readonly IWatchProgressRepository _repo;

        public WatchProgressService(IWatchProgressRepository repo)
        {
            _repo = repo;
        }

        public async Task<WatchProgressResponse> UpsertAsync(WatchProgressRequest request)
        {
            var existing = await _repo.GetAsync(request.ProfileId, request.EpisodeId);

            // CREATE
            if (existing == null)
            {
                var entity = request.Adapt<WatchProgress>();
                entity.Completed = request.ProgressSeconds >= request.DurationSeconds * 0.95;
                entity.UpdatedAt = DateTime.UtcNow;

                await _repo.AddAsync(entity);

                return entity.Adapt<WatchProgressResponse>();
            }

            // UPDATE EXISTING
            existing.ProgressSeconds = request.ProgressSeconds;
            existing.DurationSeconds = request.DurationSeconds;
            existing.Completed = request.ProgressSeconds >= request.DurationSeconds * 0.95;
            existing.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(existing);

            return existing.Adapt<WatchProgressResponse>();
        }

        public async Task<List<WatchProgressResponse>> GetForProfileAsync(Guid profileId)
        {
            var list = await _repo.GetForProfileAsync(profileId);
            return list.Adapt<List<WatchProgressResponse>>();
        }
    }

}
