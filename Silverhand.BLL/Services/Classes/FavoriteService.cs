using Mapster;
using Silverhand.BLL.Services.Interface;
using Silverhand.DAL.DTO.Requests;
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
    public class FavoriteService:IFavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepo;
        private readonly IProfileRepository _profileRepo;
        private readonly ITitleRepository _titleRepo;

        public FavoriteService(
            IFavoriteRepository favoriteRepo,
            IProfileRepository profileRepo,
            ITitleRepository titleRepo)
        {
            _favoriteRepo = favoriteRepo;
            _profileRepo = profileRepo;
            _titleRepo = titleRepo;
        }

        public async Task<bool> AddAsync(FavoriteRequest req)
        {
            // Validate profile
            var profile = await _profileRepo.GetByIdAsync(req.ProfileId);
            if (profile is null)
                return false;

            // Validate title
            var title = await _titleRepo.GetByIdAsync(req.TitleId);
            if (title is null)
                return false;

            // Duplicate check
            var existing = await _favoriteRepo.GetByProfileAndTitleAsync(req.ProfileId, req.TitleId);
            if (existing != null)
                return false;

            // Add new favorite
            var fav = new Favorite
            {
                ProfileId = req.ProfileId,
                TitleId = req.TitleId
            };

            await _favoriteRepo.AddAsync(fav);
            return true;
        }

        public async Task<bool> RemoveAsync(Guid profileId, Guid titleId)
        {
            var fav = await _favoriteRepo.GetByProfileAndTitleAsync(profileId, titleId);
            if (fav == null)
                return false;

            return await _favoriteRepo.DeleteAsync(fav.Id);
        }

        public async Task<List<FavoriteResponse>> GetAllByProfileAsync(Guid profileId)
        {
            var favorites = await _favoriteRepo.GetAllByProfileAsync(profileId);

            var response = favorites.Select(f => new FavoriteResponse
            {
                Id = f.Id,
                TitleId = f.TitleId,
                TitleName = f.Title?.Name ?? "",
                PosterUrl = f.Title?.PosterUrl ?? "",
                CreatedAt = f.CreatedAt
            }).ToList();

            return response;
        }

    }
}
